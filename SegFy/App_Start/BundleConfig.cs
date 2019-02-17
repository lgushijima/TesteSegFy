using System.Web;
using System.Web.Optimization;

namespace SegFy
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Content/js/ninjaCoreBS-pack-min.js")
                .Include("~/Content/js/ninjaCoreBS-pt-BR-min.js")
                .Include("~/Content/js/ninjaCoreBS-extension-min.js")
                .Include("~/Content/js/bootstrap-notify.js")
                .Include("~/Content/js/main.js")
                );
            

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/animate.min.css")
                .Include("~/Content/css/bootstrap.min.css")
                .Include("~/Content/css/ninjaCoreBS-pack-min.css")
                .Include("~/Content/css/site.css")
                );
        }
    }
}
