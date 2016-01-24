using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using Thunder.Web.Mvc.Binders;
using RequiredAttributeAdapter = Template.Adapters.RequiredAttributeAdapter;
using StringLengthAttributeAdapter = Template.Adapters.StringLengthAttributeAdapter;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Template.XerneasConfig), "Start")]
namespace Template
{
    public static class XerneasConfig
    {
        public static void Start()
        {
            XmlConfigurator.Configure();

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

            DataAnnotationsModelValidatorProvider.RegisterAdapterFactory(typeof(RequiredAttribute),
                (metadata, controllerContext, attribute) => new RequiredAttributeAdapter(metadata, controllerContext, (RequiredAttribute)attribute));

            DataAnnotationsModelValidatorProvider.RegisterAdapterFactory(typeof(StringLengthAttribute),
                (metadata, controllerContext, attribute) => new StringLengthAttributeAdapter(metadata, controllerContext, (StringLengthAttribute)attribute));

            DefaultModelBinder.ResourceClassKey = "Resources";
            ValidationExtensions.ResourceClassKey = "Resources";

            RouteTable.Routes.LowercaseUrls = true;

            #region Bundle 
            BundleTable.EnableOptimizations = false;

            BundleTable.Bundles.Add(new ScriptBundle("~/scripts/bundle")
                            .Include("~/scripts/jquery-{version}.js"
                                , "~/scripts/jquery-ui-{version}.js"
                                , "~/scripts/bootstrap.min.js"
                                , "~/content/sb-admin-2/dist/metisMenu/js/metisMenu.min.js"
                                , "~/content/sb-admin-2/js/sb-admin-2.js"
                                , "~/scripts/jquery.nestable.js"
                                , "~/scripts/icheck.js"
                                , "~/scripts/thunderjs-{version}.js"
                                , "~/scripts/application/app.js"
                                , "~/scripts/application/initialize.js"
                                , "~/scripts/application/login.js"
                                , "~/scripts/application/modules.js"
                                ));

            BundleTable.Bundles.Add(new StyleBundle("~/content/bundle")
                .Include("~/content/bootstrap.min.css"
                , "~/content/thunderjs-{version}.css"
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
            
            BundleTable.Bundles.Add(new StyleBundle("~/content/icheck/skins/bundle")
                .Include("~/content/icheck/skins/all.css"));
            #endregion
        }
    }
}