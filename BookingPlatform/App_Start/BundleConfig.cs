using System.Web;
using System.Web.Optimization;

namespace BookingPlatform
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",
                        "~/Scripts/jquery.validate.min.jsjs",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/wow.min.js",
                        "~/Scripts/jquery.uniform.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/touchSwipe.min.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/jquery.blueimp-gallery.min.js",
                        "~/Scripts/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                       "~/Content/jquery-ui.min.css",
                      "~/Content/custom/uniform.default.min.css",
                      "~/Content/custom/animate.css",
                      "~/Content/custom/blueimp-gallery.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/images").Include(
                      "~/Content/images/favicon.png"));

            bundles.Add(new StyleBundle("~/Content/customCss").Include(
                      "~/Content/style.css"));
        }
    }
}
