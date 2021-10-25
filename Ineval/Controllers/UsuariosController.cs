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
using System.Net.Mail;

namespace Ineval.Controllers
{
    [CustomAuthorize(ModuleName = "Usuarios")]
    public class UsuariosController : BaseController<Guid, Usuario, UsuarioViewModel>
    {
        SwmContext db = new SwmContext();
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


        public virtual async Task<ActionResult> SaveNew(UsuarioViewModel model)
        {
            OnBeginCrudAction();

            if (!ModelState.IsValid)
            {
                return await Task.Run(() => Json(new { success = false, message = GetValidationMessages() }, JsonRequestBehavior.AllowGet));
            }

            try
            {
                var entity = MapperModelToEntity(model);

                var saveResult = await EntityService.SaveAsync(entity);

                if (saveResult.Succeeded)
                {
                    EnviarCorreo(entity.Id, "Se registro un nuevo Usuario");

                    return await Task.Run(() => Json(new { success = true, message = string.Empty }, JsonRequestBehavior.AllowGet));


                }

                return await Task.Run(() => Json(new { success = false, message = saveResult.GetErrorsString() }, JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> EnviarCorreo(Guid UsuarioId, string Mensaje)
        {
            EmailParametros emailParametros = new EmailParametros();
            emailParametros = await db.EmailParametros.FirstOrDefaultAsync();

            Usuario usuario = new Usuario();
            usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Id == UsuarioId);

            MailMessage msg = new MailMessage();

            if (usuario != null)
            {
                if (IsValidEmailAddress(usuario.Email))
                {
                    msg.To.Add(new MailAddress(usuario.Email.Trim()));
                }
            }

            string[] CorreosEnviar;

            CorreosEnviar = emailParametros.EmailCopia.Split(' ');

            foreach (string email_ in CorreosEnviar)
            {
                if (IsValidEmailAddress(email_.Trim()))
                {
                    msg.CC.Add(new MailAddress(email_.Trim()));
                }
            }

            //msg.To.Add("r.caiza@reliv.la");
            msg.Subject = "Ineval";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Bcc.add

            msg.Body = Mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;





            msg.From = new MailAddress(emailParametros.EmailPrincipal);
            SmtpClient cliete = new SmtpClient();

            cliete.Port = 587;
            cliete.EnableSsl = true;

            cliete.Host = "smtp.gmail.com";
            cliete.Credentials = new NetworkCredential(emailParametros.EmailPrincipal, EncryptDecrypt.Decrypt(emailParametros.EmailPassword));


            try
            {
                cliete.Send(msg);
                return Json("Hola", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);

            }

        }

        private static bool IsValidEmailAddress(string emailAddress)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(emailAddress);
        }
    }
}