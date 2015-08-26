using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Template.XerneasConfig), "Start")]
namespace Template
{
    public static class XerneasConfig
    {
        public static void Start()
        {
            XmlConfigurator.Configure();

            RouteTable.Routes.LowercaseUrls = true;

            BundleTable.EnableOptimizations = false;

            BundleTable.Bundles.Add(new ScriptBundle("~/scripts/bundle")
                .Include("~/scripts/jquery-2.1.4.min.js"
                    , "~/scripts/jquery-ui-1.11.4.js"
                    , "~/scripts/bootstrap.min.js"
                    , "~/scripts/handlebars-v2.0.0.js"
                    , "~/content/sb-admin-2/dist/metisMenu/js/metisMenu.min.js"
                    , "~/content/sb-admin-2/js/sb-admin-2.js"
                    , "~/scripts/jquery.nestable.js"
                    , "~/scripts/thunderjs-1.0.9.js"
                    , "~/scripts/application/app.js"
                    , "~/scripts/application/initialize.js"
                    , "~/scripts/application/login.js"
                    , "~/scripts/application/modules.js"
                    ));

            BundleTable.Bundles.Add(new StyleBundle("~/content/bundle")
                .Include("~/content/bootstrap.min.css"
                , "~/content/thunderjs-1.0.9.css"
                , "~/content/jquery.nestable.css"
                ));

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