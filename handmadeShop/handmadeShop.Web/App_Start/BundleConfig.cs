using System.Web;
using System.Web.Optimization;

namespace handmadeShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css/all.css").Include("~/Vendor/css/all.css"));
            bundles.Add(new StyleBundle("~/bundles/css/baguetteBox.min.css").Include("~/Vendor/css/baguetteBox.min.css"));
            bundles.Add(new StyleBundle("~/bundles/css/bootsnav.css").Include("~/Vendor/css/bootsnav.css"));
            bundles.Add(new StyleBundle("~/bundles/css/bootstrap-select.css").Include("~/Vendor/css/bootstrap-select.css"));
            bundles.Add(new StyleBundle("~/bundles/css/bootstrap.min.css").Include("~/Vendor/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/bundles/css/carousel-ticker.css").Include("~/Vendor/css/carousel-ticker.css"));
            bundles.Add(new StyleBundle("~/bundles/css/code_animate.css").Include("~/Vendor/css/code_animate.css"));
            bundles.Add(new StyleBundle("~/bundles/css/custom.css").Include("~/Vendor/css/custom.css"));
            bundles.Add(new StyleBundle("~/bundles/css/jquery-ui.css").Include("~/Vendor/css/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/bundles/css/owl.carousel.min.css").Include("~/Vendor/css/owl.carousel.min.css"));
            bundles.Add(new StyleBundle("~/bundles/css/responsive.css").Include("~/Vendor/css/responsive.css"));
            bundles.Add(new StyleBundle("~/bundles/css/style.css").Include("~/Vendor/css/style.css"));
            bundles.Add(new StyleBundle("~/bundles/css/superslides.css").Include("~/Vendor/css/superslides.css"));
            bundles.Add(new ScriptBundle("~/bundles/js/baguetteBox.min.js").Include("~/Vendor/js/baguetteBox.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/bootsnav.js").Include("~/Vendor/js/bootsnav.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap-select.js").Include("~/Vendor/js/bootstrap-select.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap.min.js").Include("~/Vendor/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/contact-form-script.js").Include("~/Vendor/js/contact-form-script.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/custom.js").Include("~/Vendor/js/custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/form-validator.min.js").Include("~/Vendor/js/form-validator.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/images-loaded.min.js").Include("~/Vendor/js/images-loaded.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/inewsticker.js").Include("~/Vendor/js/inewsticker.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/isotope.min.js").Include("~/Vendor/js/isotope.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/jquery-3.2.1.min.js").Include("~/Vendor/js/jquery-3.2.1.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/jquery-ui.js").Include("~/Vendor/js/jquery-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/jquery.nicescroll.min.js").Include("~/Vendor/js/jquery.nicescroll.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/jquery.superslides.min.js").Include("~/Vendor/js/jquery.superslides.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/owl.carousel.min.js").Include("~/Vendor/js/owl.carousel.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/popper.min.js").Include("~/Vendor/js/popper.min.js"));
        }
    }
}
