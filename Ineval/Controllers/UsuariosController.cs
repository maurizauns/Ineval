using AutoMapper;
using MvcJqGrid;
using RP.Website.Helpers;
using Ineval.BO;
using Ineval.Common;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Extensions;
using Ineval.Models.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [CustomAuthorize(ModuleName = "Usuarios")]
    public class UsuariosController : BaseController<Guid, Usuario, UsuarioViewModel>
    {
        public UsuariosController()
        {
            Title = "Usuario";
            EntityService = new UsuarioService();
        }
        //private string EmpresaId
        //{
        //    get { return Session["empresaId"] != null ? Session["empresaId"].ToString() : ""; }
        //    set { Session["empresaId"] = value; }
        //}
        public override void OnBeginIndex()
        {
            using (var roleMenuService = new RoleMenuService())
            {
                ViewBag.ApplicationRoleName = new SelectList(roleMenuService.ObtenerRoles(), "Name", "Name", null);
            }
        }
        protected override IQueryable<Usuario> ApplyFilters(IQueryable<Usuario> generalQuery, Rule[] filters)
        {
            if (filters == null)
            {
                return generalQuery;
            }

            foreach (var item in filters)
            {
                var term = item.data.Trim().ToUpper();

                if (String.Equals(item.field, "identificacion", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Identificacion.Trim().ToLower().Contains(term));
                }
            }
            return generalQuery;
        }

        protected override string[] GetRow(Usuario item)
        {
            var row = new[]
             {
                HttpUtility.HtmlEncode(UsuarioService.GetTipoIdentificacion(item.TipoIdentificacion)),
                HttpUtility.HtmlEncode(item.Identificacion),
                HttpUtility.HtmlEncode(item.NombresCompletos),
                HttpUtility.HtmlEncode(item.ApplicationUser.Email),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("usuarios-modal")
                            .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "usuariosCallback"))
                            .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "usuarios-grid", item.Id))
                            .Add(ConfigurarAction(item.Id))
                            .End())
            };
            return row;
        }
        public IHtmlString ConfigurarAction(object id = null)
        {
            var button = string.Format(@"<li class=""""><a title=""Cambiar de Clave"" data-toggle=""tooltip"" class=""btn btn-warning btn-xs"" href=""{0}""><i class=""bx bx-key""></i></a></li>",
                            Url.Action("CambiaClave", new { id }));
            return MvcHtmlString.Create(button);
        }
        public override IEnumerable<FieldFilter> Filters
        {

            get
            {
                var filters = new List<FieldFilter>
                {
                    new FieldFilter
                    {
                        Description = "Identificación",
                        Name = "identificacion",
                        Type = FilterType.Textbox,
                    }
                };
                return filters;
            }
        }
        protected override UsuarioViewModel MapperEntityToModel(Usuario entity)
        {
            using (var roleMenuService = new RoleMenuService())
            {
                var roles = roleMenuService.ObtenerRolPorUserId(entity.ApplicationUserId);
                if (roles.Any())
                {
                    entity.ApplicationRoleName = roles.FirstOrDefault();
                }
            }

            return new UsuarioViewModel
            {
                Id = entity.Id,
                TipoIdentificacion = entity.TipoIdentificacion,
                Identificacion = entity.Identificacion,
                NombresCompletos = entity.NombresCompletos,
                Email = entity.Email,
                ApplicationRoleName = entity.ApplicationRoleName
            };
        }

        protected override Usuario MapperModelToEntity(UsuarioViewModel viewModel)
        {
            Usuario usuario = null;

            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                usuario = EntityService.GetById(viewModel.Id.Value);
            }
            else
            {
                usuario = new Usuario();
            }

            usuario.Id = viewModel.Id == null ? Guid.Empty : viewModel.Id.Value;
            usuario.TipoIdentificacion = viewModel.TipoIdentificacion;
            usuario.Identificacion = viewModel.Identificacion;
            usuario.NombresCompletos = viewModel.NombresCompletos;
            usuario.Email = viewModel.Email;
            usuario.ApplicationRoleName = viewModel.ApplicationRoleName;
            return usuario;
        }

        [HttpGet]
        public virtual async Task<ActionResult> GetUsers()
        {
            var result = await EntityService.GetAll().ToListAsync();
            List<UsuarioViewModel> resultDto = Mapper.Map<List<UsuarioViewModel>>(result);
            return Json(resultDto, JsonRequestBehavior.AllowGet);
        }

        #region profile
        public async Task<ActionResult> ConfiguracionGeneral(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = await EntityService.GetByIdAsync(id.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            var model = new UsuarioViewModel();
            model.Id = id;
            model.NombresCompletos = usuario.NombresCompletos;

            if (model != null)
            {
                ViewBag.id = model.Id;
                ViewBag.nombre = model.NombresCompletos;
            }
            return View(model);
        }

        #endregion
    }
}