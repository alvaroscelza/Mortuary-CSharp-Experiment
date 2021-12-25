using System.Web.Optimization;

namespace mortuary
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery/jquery.validate*"));
            // TODO: Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/bill-lines").Include("~/Scripts/bill-lines/bill-lines.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/bootstrap.css", "~/Content/site.css"));
        }
    }
}
