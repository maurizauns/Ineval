using MvcJqGrid;
using MvcJqGrid.Enums;
using RP.DAL.Repository;
using RP.Util.Extension;
using Ineval.BO;
using Ineval.Common;
using Ineval.DAL;
using Ineval.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Ineval.Controllers
{
    public abstract class BaseController<TKey, TEntity, TModel> : Controller, IFilter
                       where TEntity : BaseEntityClass<TKey>
                       where TModel : class
    {
        protected string Title { get; set; }

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

            return View();
        }

        public virtual async Task<ActionResult> GetList(GridSettings gridSettings)
        {
            OnBeginCrudAction();

            var page = gridSettings.PageIndex;

            var generalQuery = await EntityService.GetAllAsync();

            generalQuery = OnBeginFilter(generalQuery);

            var defaultFilters = GetDefaultFilters();

            if (defaultFilters != null && defaultFilters.Any())
            {
                var rules = defaultFilters.Select(f => new Rule()
                {
                    data = f.DefaultValue,
                    field = f.Name
                });

                generalQuery = ApplyFilters(generalQuery, rules.ToArray());
            }

            if (gridSettings.Where != null)
            {
                generalQuery = ApplyFilters(generalQuery, gridSettings.Where.rules);
            }

            var total = generalQuery.Count();
            var sort = gridSettings.SortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc;
            generalQuery = generalQuery.SortAndPage(gridSettings.SortColumn, sort, page, Context.PageSize);
            var totalPages = (int)Math.Ceiling((decimal)total / Context.PageSize);

            var data = new
            {
                total = totalPages, // Total de páginas
                page, // Página actual
                records = total, // Total de registros (obtenido del modelo)

                rows = generalQuery.ToList().Select(item => new
                {
                    id = item.Id, // ID único de la fila
                    cell = GetRow(item)
                })
            };

            return Json(data, JsonRequestBehavior.AllowGet);
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