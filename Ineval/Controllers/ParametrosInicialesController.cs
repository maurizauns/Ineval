using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using MvcJqGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ParametrosInicialesController : BaseController<Guid, ParametrosIniciales, ParametrosInicialesViewModel>
    {
        SwmContext db = new SwmContext();
        public ParametrosInicialesController()
        {
            Title = "Test";
            EntityService = new ParametrosInicialesService();
        }

        protected override IQueryable<ParametrosIniciales> ApplyFilters(IQueryable<ParametrosIniciales> generalQuery, Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(ParametrosIniciales item)
        {
            throw new NotImplementedException();
        }

        protected override ParametrosInicialesViewModel MapperEntityToModel(ParametrosIniciales entity)
        {
            return Mapper.Map<ParametrosIniciales, ParametrosInicialesViewModel>(entity);
        }

        protected override ParametrosIniciales MapperModelToEntity(ParametrosInicialesViewModel viewModel)
        {
            var parametrosIniciales = new ParametrosIniciales();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                parametrosIniciales = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, parametrosIniciales);
        }
    }
}