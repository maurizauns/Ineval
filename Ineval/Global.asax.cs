using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ineval.DAL.Migrations;
using AutoMapper;
using Ineval.App_Start;

namespace Ineval
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<SwmContext, Configuration>());
            }
            Mapper.Initialize(c => c.AddProfile(new MaperConfig()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ConfiguracionGeneralConfig());

            InevalConfig.Init();
        }
    }
}
