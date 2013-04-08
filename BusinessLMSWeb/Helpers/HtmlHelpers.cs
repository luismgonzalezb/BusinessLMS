using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace BusinessLMSWeb.Helpers
{
	public static class HtmlHelpers
	{
		public static HtmlString CheckBoxMetroFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
		{

			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			string displayName = metadata.DisplayName;
			string propertyName = metadata.PropertyName;
			bool required = metadata.IsRequired;
			bool validate = metadata.RequestValidationEnabled;
			bool checkedd = metadata.Model != null ? (bool)metadata.Model : false;
			//Create tag
			TagBuilder divTag = new TagBuilder("label");
			divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

			StringBuilder inputTag = new StringBuilder();

			inputTag.Append(string.Concat("<input type=\"checkbox\" name=\"", propertyName, "\" "));
			inputTag.Append(string.Concat(" id=\"", propertyName, "\" "));
			if (required == true)
			{
				inputTag.Append(string.Concat(" data-val-required=\"The ", displayName, " field is required.\" "));
			}
			if (validate == true)
			{
				inputTag.Append(string.Concat(" data-val=\"true\" "));
			}
			if (checkedd == true)
			{
				inputTag.Append(string.Concat(" checked "));
			}
			inputTag.Append(string.Concat(" value=\"true\" >"));
			inputTag.Append(string.Concat("<span>", displayName, "</span>"));

			divTag.InnerHtml = inputTag.ToString();

			return MvcHtmlString.Create(divTag.ToString());

		}

		public static MvcHtmlString Script(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
		{
			htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
			return MvcHtmlString.Empty;
		}

		public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
		{
			foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
			{
				if (key.ToString().StartsWith("_script_"))
				{
					var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
					if (template != null)
					{
						htmlHelper.ViewContext.Writer.Write(template(null));
					}
				}
			}
			return MvcHtmlString.Empty;
		}

		public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
		{
			var values = from TEnum e in Enum.GetValues(typeof(TEnum))
						 select new { Id = e, Name = e.ToString() };

			return new SelectList(values, "Id", "Name", enumObj);
		}

	}

}