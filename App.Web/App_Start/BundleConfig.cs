using System.Web;
using System.Web.Optimization;

namespace AppProj.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/utopia").Include(
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/bootstrap-datatable.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/custom.main.js",
                        "~/Scripts/custom.imagegrid.js",
                        "~/Scripts/custom.datatable.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/js/jquery.cookie.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/jquery.jqprint.0.3.js",
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/js/header.js",
                        "~/Scripts/js/sidebar.js",
                        "~/Scripts/jquery-barcode.js",
                        "~/Scripts/printThis.js",
                        "~/Scripts/exportExcell.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/existing").Include(
                         "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/bootstrap-datatable.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/custom.main.js",
                        "~/Scripts/custom.imagegrid.js",
                        "~/Scripts/custom.datatable.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/js/jquery.cookie.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/jquery.jqprint.0.3.js",
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/js/header.js",
                        "~/Scripts/js/sidebar.js",
                        "~/Scripts/jquery-barcode.js",
                        "~/Scripts/printThis.js",
                        "~/Scripts/exportExcell.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/bracjs").Include(
                       //"~/Scripts/brac/app.js",
                       "~/Scripts/brac/site.js"
                       ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                        "~/Content/css/utopia-white.css",
                        "~/Content/css/utopia-responsive.css",
                "~/Content/css/social_icon/icons.css",
                        "~/Content/custom.css",
                        "~/Content/typeahead.css",
                        "~/Content/bootstrap-datepicker.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/brac").Include(
                        "~/Content/css/brac/bundle.css",
                        "~/Content/typeahead.css",
                        "~/Content/bootstrap-datepicker.css",
                         "~/Content/css/brac/site.css"
                        ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}