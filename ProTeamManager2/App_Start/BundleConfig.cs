using System.Web;
using System.Web.Optimization;

namespace ProTeamManager2
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Team.js").Include(
                       "~/Scripts/Team.js"));
            bundles.Add(new ScriptBundle("~/bundles/Release.js").Include(
                      "~/Scripts/Release.js"));
            bundles.Add(new ScriptBundle("~/bundles/Sprint.js").Include(
                      "~/Scripts/Sprint.js"));
            bundles.Add(new ScriptBundle("~/bundles/Userstory.js").Include(
                     "~/Scripts/Userstory.js"));
            bundles.Add(new ScriptBundle("~/bundles/Task.js").Include(
                     "~/Scripts/Task.js"));
            bundles.Add(new ScriptBundle("~/bundles/rate.js").Include(
                     "~/Scripts/rate.js"));
            bundles.Add(new ScriptBundle("~/bundles/Userdashboard.js").Include(
                    "~/Scripts/Userdashboard.js"));
            bundles.Add(new ScriptBundle("~/bundles/mybundle").Include(
            "~/Scripts/jquery-1.11.1.min.js",
            "~/Scripts/jquery.js",
            "~/Scripts/jquery-ui.js",
            "~/Scripts/jquery-ui-1.10.4.custom.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/mystyles").Include("~/Styles/jquery-ui-1.10.4.custom.css",
                "~/Styles/jquery-ui.css",
                "~/Styles/Index.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}