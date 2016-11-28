using System.Web;
using System.Web.Optimization;

namespace Acctive
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //"~/vendors/jquery/dist/jquery.js"));
                "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/nprogress.js",
                "~/build/js/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js",
                "~/Scripts/DataTables/dataTables.buttons.js",
                "~/Scripts/DataTables/buttons.bootstrap.js",
                "~/Scripts/DataTables/buttons.print.js",
                "~/Scripts/DataTables/dataTables.fixedHeader.js",
                "~/Scripts/DataTables/dataTables.keyTable.js",
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/Scripts/DataTables/responsive.bootstrap.js",
                "~/Scripts/DataTables/datatables.scroller.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                "~/Scripts/jquery.icheck.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/smartwizard").Include(
                "~/Scripts/jQuery-Smart-Wizard/jquery.smartWizard.js",
                "~/Scripts/FastClick/fastclick.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Scripts/moment.js",
                "~/Scripts/daterangepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/nprogress.css",
                "~/Content/animate.css",
                "~/Content/iCheck/flat/green.css",
                "~/build/css/custom.css"));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                /*"~/Content/DataTables/css/jquery.dataTables.min.css",*/
                "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                "~/Content/DataTables/css/buttons.bootstrap.min.css",
                "~/Content/DataTables/css/fixedHeader.bootstrap.min.css",
                "~/Content/DataTables/css/responsive.bootstrap.min.css",
                "~/Content/DataTables/css/scroller.bootstrap.min.css"
                ));
        }
    }
}