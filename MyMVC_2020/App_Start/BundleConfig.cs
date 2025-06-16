using System.Web;
using System.Web.Optimization;

namespace MyMVC_2020
{
    public class BundleConfig
    {
        // 如需 Bundling 的詳細資訊，請造訪 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;

            #region Script
            bundles.Add(new ScriptBundle("~/Scripts/jquery")
                .Include(
                "~/Scripts/jquery-2.2.0.*",
             "~/Scripts/jquery-2.2.0.intellisense.js"
             
                ));
            /*
             "~/Scripts/jquery-{version}.min.js"                                
             "~/Scripts/jquery-{version}.intellisense.js",
             "~/Scripts/jquery-{version}.js"
             */

            bundles.Add(new ScriptBundle("~/Content/js/jquery", "http://code.jquery.com/jquery-1.12.0.min.js")
                .Include("~/Content/js/jquery-1.12.0.js"));

            bundles.Add(new ScriptBundle("~/Content/js/BootStrap", "//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js")
                .Include("~/Content/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Content/js/script")
                .Include("~/Content/js/script.js"));

            bundles.Add(new ScriptBundle("~/Content/js/countdown")
                .Include("~/Content/js/jquery.countdown.js"));

            bundles.Add(new ScriptBundle("~/Content/js/kendo", "//cdn.kendostatic.com/2016.1.112/js/kendo.all.min.js")
                .Include("~/Content/js/kendo.all.min.js"));

            #endregion

            #region CSS

            bundles.Add(new StyleBundle("~/Content/css/BootStrap", "//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css")
                .Include("~/Content/css/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css/Font-Awesome", "//netdna.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css")
                .Include("~/Content/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/css/style")
                .Include("~/Content/css/build.css")
                .Include("~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/css/kendo-common", "//cdn.kendostatic.com/2016.1.112/styles/kendo.common.min.css")
                .Include("~/Content/css/kendo.common.min.css"));
            bundles.Add(new StyleBundle("~/Content/css/kendo-default", "//cdn.kendostatic.com/2016.1.112/styles/kendo.default.min.css")
                .Include("~/Content/css/kendo.default.min.css"));

            #endregion

            #region 自定忽略清單

            //bundles.IgnoreList.Clear();
            //bundles.IgnoreList.Ignore("*.min.js", OptimizationMode.WhenEnabled);
            ////bundles.IgnoreList.Ignore("*.min.css", OptimizationMode.WhenEnabled);
            //bundles.IgnoreList.Ignore("*.intellisense.js");
            //bundles.IgnoreList.Ignore("*-vsdoc.js");
            //bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);

            #endregion            
        }
    }
}