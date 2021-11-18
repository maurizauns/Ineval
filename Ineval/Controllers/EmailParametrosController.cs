using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using MvcJqGrid;
using RP.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class EmailParametrosController : BaseController<Guid, EmailParametros, EmailParametrosViewModel>
    {
        public EmailParametrosController()
        {
            EntityService = new EmailParametrosService();
            Title = "Configuracion de parametros de Correos";
        }
        protected override IQueryable<EmailParametros> ApplyFilters(IQueryable<EmailParametros> generalQuery, Rule[] filters)
        {
            if (filters == null)
            {
                return generalQuery;
            }

            foreach (var item in filters)
            {
                var term = item.data.Trim().ToUpper();

                if (String.Equals(item.field, "codigo", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Code.Trim().ToUpper().Contains(term));
                }
                else if (String.Equals(item.field, "descripcion", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Description.Trim().ToLower().Contains(term));
                }
            }
            return generalQuery;
        }

        protected override string[] GetRow(EmailParametros item)
        {
            return new[]
            {
                HttpUtility.HtmlEncode(item.EmailPrincipal),
                HttpUtility.HtmlEncode(item.EmailPassword),
                HttpUtility.HtmlEncode(item.EmailCopia),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("emailparametros-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntityNew"), item.Id, "emailparametrosCallback"))
                        .End())
            };
        }

        protected override EmailParametrosViewModel MapperEntityToModel(EmailParametros entity)
        {
            return Mapper.Map<EmailParametros, EmailParametrosViewModel>(entity);
        }

        protected override EmailParametros MapperModelToEntity(EmailParametrosViewModel viewModel)
        {
            var emailparametros = new EmailParametros();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                emailparametros = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, emailparametros);
        }

        public virtual async Task<ActionResult> SaveNew(EmailParametrosViewModel model)
        {
            OnBeginCrudAction();

            int existe = EntityService.GetAll().Count();
            if (existe == 0)
            {
                if (!ModelState.IsValid)
                {
                    return await Task.Run(() => Json(new { success = false, message = GetValidationMessages() }, JsonRequestBehavior.AllowGet));
                }

                try
                {
                    var entity = MapperModelToEntity(model);
                    entity.EmailPassword = EncryptDecrypt.Encrypt(entity.EmailPassword);
                    var saveResult = await EntityService.SaveAsync(entity);

                    if (saveResult.Succeeded)
                    {
                        return await Task.Run(() => Json(new { success = true, message = string.Empty }, JsonRequestBehavior.AllowGet));
                    }

                    return await Task.Run(() => Json(new { success = false, message = saveResult.GetErrorsString() }, JsonRequestBehavior.AllowGet));
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (model.Id != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return await Task.Run(() => Json(new { success = false, message = GetValidationMessages() }, JsonRequestBehavior.AllowGet));
                    }

                    try
                    {
                        var entity = MapperModelToEntity(model);
                        entity.EmailPassword = EncryptDecrypt.Encrypt(entity.EmailPassword);
                        var saveResult = await EntityService.SaveAsync(entity);

                        if (saveResult.Succeeded)
                        {
                            return await Task.Run(() => Json(new { success = true, message = string.Empty }, JsonRequestBehavior.AllowGet));
                        }

                        return await Task.Run(() => Json(new { success = false, message = saveResult.GetErrorsString() }, JsonRequestBehavior.AllowGet));
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Ya existe una configuración por favor editela..." }, JsonRequestBehavior.AllowGet);
                }
                
            }
        }


        public virtual async Task<ActionResult> GetEntityNew(Guid id)
        {
            OnBeginCrudAction();

            var entity = await EntityService.GetByIdAsync(id);

            if (entity == null)
            {
                return await Task.Run(() => Json(new { success = false, message = "No Existe Registro" }, JsonRequestBehavior.AllowGet));
            }

            entity.EmailPassword = EncryptDecrypt.Decrypt(entity.EmailPassword);

            return await Task.Run(() => Json(MapperEntityToModel(entity), JsonRequestBehavior.AllowGet));
        }

    }
}