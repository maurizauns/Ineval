using Ineval.BO;
using Ineval.DAL;
using Ineval.Models.Filters;
using MvcJqGrid;
using RP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Ineval.Controllers
{
    public abstract class BaseProcesoController<TKey, TEntity, TModel> : Controller, IFilter
                     where TEntity : BaseEntityClass<TKey>
                     where TModel : class
    {
        protected string Title { get; set; }
        protected Guid AdmissionId { get; set; }

        protected IEntityService<TKey, TEntity> EntityService;
        protected abstract IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> generalQuery, Rule[] filters);
        protected abstract string[] GetRow(TEntity item);
        protected abstract TEntity MapperModelToEntity(TModel viewModel);
        protected abstract TModel MapperEntityToModel(TEntity entity);

        protected List<FieldFilter> GetDefaultFilters()
        {
            return Filters.Where(f => f.IsDefaultValue).ToList();
        }

        public virtual ActionResult Index()
        {
            OnBeginIndex();
            string url = Request.UrlReferrer.LocalPath.ToString().Split('/').Last();//Request.UrlReferrer.OriginalString.ToString().Split('/').Last(); //;
            HttpContext.Items.Add("urlPadre", url);
            if (Request.UrlReferrer.Query != "")
            {
                url = Request.UrlReferrer.Query.ToString().Split('?').Last();
                if (url.Contains("&"))
                {
                    url = url.Split('&').First();
                }
                url = url.Replace("personId=", "");
                //url = url.Contains("isEvolution") ? url.Replace("&isEvolution=True", "") : url;
            }


            if (url.Contains("?"))
            {
                url = url.Replace("register?personId=", "");
            }
            AdmissionId = new Guid(url);
            return PartialView();
        }

        public virtual async Task<ActionResult> Save(TModel model)
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
                    return await Task.Run(() => Json(new { success = true, message = string.Empty }, JsonRequestBehavior.AllowGet));
                }

                return await Task.Run(() => Json(new { success = false, message = saveResult.GetErrorsString() }, JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public virtual async Task<ActionResult> GetEntity(TKey id)
        {
            OnBeginCrudAction();

            var entity = await EntityService.GetByIdAsync(id);

            if (entity == null)
            {
                return await Task.Run(() => Json(new { success = false, message = "No Existe Registro" }, JsonRequestBehavior.AllowGet));
            }

            return await Task.Run(() => Json(MapperEntityToModel(entity), JsonRequestBehavior.AllowGet));
        }
        public virtual async Task<JsonResult> Delete(TKey id)
        {
            try
            {
                OnBeginCrudAction();

                var saveResult = await EntityService.DeleteAsync(id);

                // var message = saveResult.Succeeded ? ResourceMessage.EliminacionSatisfactoria : saveResult.GetErrorsString();
                var message = saveResult.Succeeded ? "Eliminación Satisfactoria." : saveResult.GetErrorsString();
                OnDeleted(id, saveResult, ref message);

                return await Task.Run(() => Json(new { success = true, message }, JsonRequestBehavior.AllowGet));

            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        protected string GetValidationMessages()
        {
            var messages = "";
            if (ModelState.IsValid) return messages;
            foreach (var value in ModelState.Values)
            {
                value.Errors.ForEach(err => messages = string.Concat(messages, err.ErrorMessage, "<br />"));
            }
            return messages;
        }
        public virtual IEnumerable<FieldFilter> Filters
        {
            get { return new List<FieldFilter>(); }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Filters = Filters;
            ViewBag.Title = Title;
            ViewBag.MenuSecondary = true;
            ViewBag.AdmissionId = AdmissionId;
            base.OnActionExecuted(filterContext);
        }

        public virtual IQueryable<TEntity> OnBeginFilter(IQueryable<TEntity> generalQuery)
        {
            return generalQuery;
        }

        public virtual void OnBeginCrudAction()
        {
        }
        public virtual void OnBeginIndex()
        {
        }

        public virtual void OnDeleted(TKey id, SaveResult saveResult, ref string message)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (EntityService != null)
                {
                    EntityService.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}