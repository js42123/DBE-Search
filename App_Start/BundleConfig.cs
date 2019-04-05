using System.Web;
using System.Web.Optimization;

namespace DBESearch
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            //bundles.Add(new ScriptBundle("~/scripts/site").Include(
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryui").Include(
               "~/Scripts/jquery-ui-{version}.js",
               "~/Scripts/jquery-ui.helpers.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryui-selector")
                .Include("~/Scripts/select2.js", new CssRewriteUrlTransform())
                .Include("~/Scripts/select2.helpers.js", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
          
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap")
                .Include("~/Scripts/bootstrap.min.js", new CssRewriteUrlTransform())
                .Include("~/Scripts/bootstrap-multiselect.js", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-multipleselect.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-dropdown").Include(
               "~/Scripts/dropdown.js"));
            bundles.Add(new ScriptBundle("~/Scripts/site").Include("~/Scripts/site-shared.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/styles/bootstrap").Include(
                "~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-theme.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-multiselect.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/Content/themes/metro/jquery-dropdown").Include("~/Content/themes/metro/dropdown.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/jqueryui").Include("~/Content/themes/base/jquery-ui.css","~/Content/themes/base/jquery.ui.all.css","~/Content/themes/base/jquery.ui.autocomplete"));

            bundles.Add(new StyleBundle("~/Content/themes/metro/jqueryui-selector").Include("~/Content/themes/Metro/select2.css"));

            bundles.Add(new StyleBundle("~/Styles/CommonCSS").Include("~/Content/Common.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/updated").Include("~/Content/updated", new CssRewriteUrlTransform())
                .Include("~/Content/FiscalUpdate.css", new CssRewriteUrlTransform()));
        }
    }
}
