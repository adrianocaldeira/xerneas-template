using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Template.XerneasConfig), "Start")]
namespace Template
{
    public static class XerneasConfig
    {
        public static void Start()
        {
            BundleTable.EnableOptimizations = true;

            BundleTable.Bundles.Add(new ScriptBundle("~/scripts/bundle")
                .Include("~/scripts/jquery-2.1.4.min.js")
                .Include("~/scripts/bootstrap.min.js")
                .Include("~/content/sb-admin-2/dist/metisMenu/js/metisMenu.min.js")
                .Include("~/content/sb-admin-2/js/sb-admin-2.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/content/bundle")
                .Include("~/content/bootstrap.min.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/content/sb-admin-2/dist/metisMenu/css/bundle")
                .Include("~/content/sb-admin-2/dist/metisMenu/css/metisMenu.min.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/content/sb-admin-2/css/bundle")
                .Include("~/content/sb-admin-2/css/timeline.css")
                .Include("~/content/sb-admin-2/css/sb-admin-2.css")
                .Include("~/content/sb-admin-2/css/sb-admin-2-custom.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/content/sb-admin-2/dist/font-awesome/css/bundle")
                .Include("~/content/sb-admin-2/dist/font-awesome/css/font-awesome.min.css"));
        }
    }
}