using System.Web;
using System.Web.Optimization;

namespace NEMILTEC.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jQuery/jquery-{version}.js",
                "~/Scripts/jQuery/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jQuery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularModules").Include(
                "~/Scripts/Angular/Modules/infinite-scroll.js",
                "~/Scripts/Angular/Modules/FileUpload/ng-file-upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/Util").Include(
                 "~/Scripts/Util/ByteBuffer/bytebuffer.js",
                "~/Scripts/Util/ProtoBuf/protobuf.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/semantic").Include(
                "~/Scripts/semantic.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Bootstrap/css/bootstrap.css",
                "~/Content/materialize/css/materialdesignicons.css",
                "~/Content/Semantic/semantic.css",
                "~/Content/colors.css",
                "~/Content/Fontawesome/css/font-awesome.css",
                "~/Content/Site.css"
               ));


        }
    }
}
