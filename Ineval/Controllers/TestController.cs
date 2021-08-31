using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TestController : BaseController<Guid, Test, TestViewModel>
    {
        public TestController()
        {
            Title = "Test";
            EntityService = new TestService();
        }
        protected override IQueryable<Test> ApplyFilters(IQueryable<Test> generalQuery, MvcJqGrid.Rule[] filters)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetRow(Test item)
        {
            throw new NotImplementedException();
        }

        protected override TestViewModel MapperEntityToModel(Test entity)
        {
            throw new NotImplementedException();
        }

        protected override Test MapperModelToEntity(TestViewModel viewModel)
        {
            throw new NotImplementedException();
        }


        public async Task<JsonResult> GetData()
        {
            
            List<Test> tests = new List<Test>();

            for (int i = 0; i < 40000; i++)
            {
                tests.Add(new Test { Name = i.ToString() });
            }           

            using (var ctx = new SwmContext())
            {                
                ctx.BulkInsert(tests.ToList());
            }           

            //using (var ctx = new SwmContext())
            //{
            //    ctx.Test.AddRange(tests);
            //    await ctx.SaveChangesAsync();
            //}

            //await EntityService.CreateRangeAsync(tests);

            var list4 = await EntityService.GetAll().ToListAsync();

            return Json("");
        }
    }
}