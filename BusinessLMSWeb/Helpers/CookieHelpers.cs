using System;
using System.Web;
using Newtonsoft.Json;

namespace BusinessLMS.Helpers
{
	public class CookieHelper
	{

		private string _name;
		private HttpCookie _cookie;

		public CookieHelper(string name, double exprires)
		{
			_name = name;
			_cookie = new HttpCookie(_name);
			_cookie.Expires = DateTime.Now.AddDays(exprires);
		}

		public bool Exists()
		{
			if ((_cookie != null) && (_cookie.Value != null)) return true;
			return false;
		}

		public void SetCookie<T>(HttpResponseBase Response, T value)
		{
			string temp = JsonConvert.SerializeObject(value);
			_cookie.Value = temp;
			Response.Cookies.Add(_cookie);
		}

		public T GetCookie<T>()
		{
			if (this.Exists() == true)
			{
				string temp = _cookie.Value;
				return JsonConvert.DeserializeObject<T>(temp);
			}
			else
			{
				return default(T);
			}
		}
	}
}