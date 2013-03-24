using System.Web.Optimization;

namespace BusinessLMSWeb
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				//"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.kwicks*",
						"~/Scripts/jquery.easing*",
						"~/Scripts/jquery.simplemodal*",
						"~/Scripts/jquery.watermark*",
						"~/Scripts/jquery.showLoading*",
						"~/Scripts/DataTables-1.9.4/media/js/jquery*"));

			bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
						"~/Scripts/fullcalendar*",
						"~/Scripts/gcal*"));

			bundles.Add(new ScriptBundle("~/bundles/noty").Include(
						"~/Scripts/noty/jquery.noty.js",
						"~/Scripts/noty/layouts/bottom.js",
						"~/Scripts/noty/themes/default.js"));

			/*
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"
						));
			*/

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryupload").IncludeDirectory("~/Scripts/FileUpload", "*.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/Metro.css",
						"~/Content/Site.css",
						"~/Content/fullcalendar.css"
						));

			bundles.Add(new ScriptBundle("~/Content/main/css").Include(
						"~/Content/main/main.css"));

			bundles.Add(new ScriptBundle("~/bundles/dropdown").Include(
				"~/Scripts/jquery.dropdown*"
				));

			bundles.Add(new StyleBundle("~/Content/themes/custom/css").Include(
						"~/Content/themes/custom-theme/jquery.ui.css"));

			BundleTable.EnableOptimizations = false;
		}
	}
}