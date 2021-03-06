﻿using System.Web;
using System.Web.Optimization;

namespace E_ticaret
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //* Admin *//

            bundles.Add(new StyleBundle("~/assets/css/styles.css").Include(
                      "~/assets/css/styles.css",
                      "~/assets/demo/variations/header-blue.css"));

            bundles.Add(new StyleBundle("~/assets/css/ie8.css").Include(
                      "~/assets/css/ie8.css"));

            bundles.Add(new ScriptBundle("~/assets/js/plugin-before").Include(
                      "~/assets/js/jqueryui-1.10.3.min.js",
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/enquire.js",
                      "~/assets/js/jquery.cookie.js",
                      "~/assets/js/jquery.nicescroll.min.js"));

            bundles.Add(new ScriptBundle("~/assets/js/plugin-after").Include(
                      "~/assets/js/placeholdr.js",
                      "~/assets/js/application.js"));

            bundles.Add(new ScriptBundle("~/Template/js/master_core.js").Include(
                      "~/Template/js/master_core.js"));

            bundles.Add(new ScriptBundle("~/Template/js/showLoading.js").Include(
                     "~/Template/js/showLoading.js"));

            bundles.Add(new StyleBundle("~/Template/css/master_styles.css").Include(
                      "~/Template/css/master_styles.css"));


        }
    }
}
