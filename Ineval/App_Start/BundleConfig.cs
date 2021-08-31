using System.Web;
using System.Web.Optimization;

namespace Ineval
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/util").Include(
                        "~/Scripts/toastr.js",
                        "~/Scripts/apprise.js",
                        "~/Scripts/culture.js",
                        "~/Scripts/util.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/moment-with-locales.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/blockUI.min.js",
                       "~/Scripts/jquery.maskedinput.js",
                       "~/Scripts/jquery.inputmask.bundle.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                      "~/Scripts/jquery.jqGrid.min.js",
                      "~/Scripts/jqgridTool.js"));

            bundles.Add(new ScriptBundle("~/bundles/Admin").Include(
                    "~/Scripts/Admin/adminlte.min.js",
                    "~/Scripts/Admin/demo.js",
                    "~/Scripts/Scripts.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base").Include(
                "~/Content/themes/base/jquery.ui.base.css",
                "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css",
                      "~/Content/jqGrid.bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/apprise.css",
                      "~/Content/site.css",
                      "~/Content/selectize.css",
                      "~/Content/colors.css",
                      "~/Content/icheck-bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/other").Include(
                      "~/Content/themes/base/jquery.ui.css",
                      "~/fonts/LivIconsEvo/LivIconsEvo.css",
                      "~/Content/select2.css",
                      "~/Script/template/sweetalert2/dist/sweetalert2.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/Basico").Include(
                    "~/Scripts/Pages/Basico/vendor.js",
                    "~/Scripts/Pages/Basico/jquery.contextMenu.js",
                    "~/Scripts/Pages/Basico/jquery-ui.js",
                    "~/Scripts/Pages/Basico/objectfield.js",
                    "~/Scripts/Pages/Basico/masterdetail.js",
                    "~/Scripts/Pages/Basico/bootbox.js",
                    "~/Scripts/Pages/Basico/selectize.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/LivIconsEvo").Include(
                    "~/fonts/LivIconsEvo/js/LivIconsEvo.tools.min.js",
                    "~/fonts/LivIconsEvo/js/LivIconsEvo.defaults.min.js",
                    "~/fonts/LivIconsEvo/js/LivIconsEvo.min.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/other").Include(
                   "~/Scripts/template/adminlte.js",
                   "~/Scripts/template/demo.js",
                   "~/Scripts/custom.js",
                   "~/Scripts/nprogress.js",
                   "~/Scripts/jquery-ui.js",
                   "~/Scripts/knockout.debug.js",
                   "~/Scripts/knockout.mapping.js",
                   "~/Script/template/sweetalert2/dist/sweetalert2.min.js"                   
             ));

            bundles.Add(new ScriptBundle("~/bundles/modalStatic").Include(
                    "~/Scripts/Pages/Basico/vendor.js",
                    "~/Scripts/Pages/Basico/selectize.js",
                    "~/Scripts/Pages/Basico/jquery-ui.js",
                    "~/Scripts/Pages/Basico/jquery.form.js",
                    "~/Scripts/Pages/Basico/jquery.tablesorter.min.js",
                    "~/Scripts/Pages/Basico/jquery.tablesorter.widgets.min.js",
                    "~/Scripts/Pages/Basico/shell.js"
              ));

            

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
