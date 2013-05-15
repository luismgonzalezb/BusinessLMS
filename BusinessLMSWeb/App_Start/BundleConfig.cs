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
						"~/Scripts/Plugins/jquery.kwicks*",
						"~/Scripts/Plugins/jquery.easing*",
						"~/Scripts/Plugins/jquery.simplemodal*",
						"~/Scripts/Plugins/jquery.watermark*",
						"~/Scripts/Plugins/jquery.showLoading*",
						"~/Scripts/Plugins/jquery.moment*",
						"~/Scripts/Plugins/DataTables-1.9.4/media/js/jquery*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/Plugins/jquery.unobtrusive*",
						"~/Scripts/Plugins/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
						"~/Scripts/Plugins/fullcalendar*",
						"~/Scripts/Plugins/gcal*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryupload").IncludeDirectory("~/Scripts/Plugins/FileUpload", "*.js"));

			bundles.Add(new ScriptBundle("~/bundles/dropdown").Include("~/Scripts/Plugins/jquery.dropdown*"));

			bundles.Add(new ScriptBundle("~/bundles/noty").Include(
						"~/Scripts/Plugins/noty/jquery.noty.js",
						"~/Scripts/Plugins/noty/layouts/bottom.js",
						"~/Scripts/Plugins/noty/themes/default.js"));

			bundles.Add(new ScriptBundle("~/bundles/pages/home").Include(
				"~/Scripts/Plugins/jquery.prettyPhoto.js",
				"~/Scripts/Plugins/jquery.flow.1.2.js",
				"~/Scripts/Plugins/jquery.showLoading.js",
				"~/Scripts/Home/Index.js"));

			#endregion

			#region Styles

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/Metro.css",
						"~/Content/Site.css",
						"~/Content/fullcalendar.css"
						));

			bundles.Add(new StyleBundle("~/Content/themes/custom/css").Include(
						"~/Content/themes/custom/jquery.ui.css"));

			bundles.Add(new StyleBundle("~/Content/main/css").Include(
						"~/Content/main/main.css"));

			#endregion

			BundleTable.EnableOptimizations = false;
		}
	}
}