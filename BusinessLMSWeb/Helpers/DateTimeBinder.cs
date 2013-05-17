using System;
using System.Globalization;
using System.Web.Mvc;

namespace BusinessLMSWeb.Helpers
{
	public class DateTimeBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			var date = new Object();
			try
			{
				date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
			}
			catch
			{
				date = value.ConvertTo(typeof(DateTime), new CultureInfo("en-US"));
			}
			return date;
		}
	}
	public class NullableDateTimeBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value != null)
			{
				var date = new Object();
				try
				{
					date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
				}
				catch
				{
					date = value.ConvertTo(typeof(DateTime), new CultureInfo("en-US"));
				}
				return date;
			}
			return null;
		}
	}
}