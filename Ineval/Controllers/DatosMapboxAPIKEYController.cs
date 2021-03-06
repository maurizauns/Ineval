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
    public class DatosMapboxAPIKEYController : BaseController<Guid, DatosMapboxAPIKEY, DatosMapboxAPIKEYViewModel>
    {
        public DatosMapboxAPIKEYController()
        {
            Title = "Datos Mapbox";
            EntityService = new DatosMapboxAPIKEYService();
        }
        protected override IQueryable<DatosMapboxAPIKEY> ApplyFilters(IQueryable<DatosMapboxAPIKEY> generalQuery, Rule[] filters)
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

        protected override string[] GetRow(DatosMapboxAPIKEY item)
        {
            return new[]
            {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(item.APIKEY),
                HttpUtility.HtmlEncode(item.NumeroMaximoConsulta),
                HttpUtility.HtmlEncode(item.NumeroMininoConsulta),
                HttpUtility.HtmlEncode(item.NumeroUsadasConsultas),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("mapbox-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "mapboxCallback"))
                        .End())
            };
        }

        protected override DatosMapboxAPIKEYViewModel MapperEntityToModel(DatosMapboxAPIKEY entity)
        {
            return Mapper.Map<DatosMapboxAPIKEY, DatosMapboxAPIKEYViewModel>(entity);
        }

        protected override DatosMapboxAPIKEY MapperModelToEntity(DatosMapboxAPIKEYViewModel viewModel)
        {
            var mapboxApiKey = new DatosMapboxAPIKEY();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                mapboxApiKey = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, mapboxApiKey);
        }


        public virtual async Task<ActionResult> SaveDatosMapBox(DatosMapboxAPIKEYViewModel model)
        {
            OnBeginCrudAction();

            if (!ModelState.IsValid)
            {
                return await Task.Run(() => Json(new { success = false, message = GetValidationMessages() }, JsonRequestBehavior.AllowGet));
            }


            if (model.Id == null)
            {              

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
            else
            {                
                try
                {
                    var result = EntityService.GetAll().Where(x => x.Id == model.Id).FirstOrDefault();
                    model.NumeroUsadasConsultas = result.NumeroUsadasConsultas;
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
        }
    }
}