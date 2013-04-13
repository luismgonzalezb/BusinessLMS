using System.Web.Optimization;

namespace BusinessLMSWeb
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{

			#region Scripts

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery.kwicks*",
						"~/Scripts/jquery.easing*",
						"~/Scripts/jquery.simplemodal*",
						"~/Scripts/jquery.watermark*",
						"~/Scripts/jquery.showLoading*",
						"~/Scripts/jquery.moment*",
						"~/Scripts/DataTables-1.9.4/media/js/jquery*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
						"~/Scripts/fullcalendar*",
						"~/Scripts/gcal*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryupload").IncludeDirectory("~/Scripts/FileUpload", "*.js"));

			bundles.Add(new ScriptBundle("~/bundles/dropdown").Include("~/Scripts/jquery.dropdown*"));

			bundles.Add(new ScriptBundle("~/bundles/noty").Include(
						"~/Scripts/noty/jquery.noty.js",
						"~/Scripts/noty/layouts/bottom.js",
						"~/Scripts/noty/themes/default.js"));

			bundles.Add(new ScriptBundle("~/bundles/pages/home").Include(
				"~/Scripts/jquery.prettyPhoto.js",
				"~/Scripts/jquery.flow.1.2.js",
				"~/Scripts/jquery.showLoading.min.js",
				"~/Scripts/Home/Index.js"));

			#endregion

			#region Styles

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/Metro.css",
						"~/Content/Site.css",
						"~/Content/fullcalendar.css"
						));

			bundles.Add(new StyleBundle("~/Content/main/css").Include(
						"~/Content/main/main.css"));

			bundles.Add(new StyleBundle("~/Content/themes/custom/css").Include(
						"~/Content/themes/custom/jquery.ui.css"));

			#endregion

			BundleTable.EnableOptimizations = false;
		}
	}
}