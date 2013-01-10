using System;
using System.Web;
using Newtonsoft.Json;

namespace BusinessLMS.Helpers
{
	public class CookieHelper
	{

		private string _name;
		private HttpCookie _cookie;
		private HttpContext _context;

		public CookieHelper(HttpContext current, string name, double exprires)
		{
			_context = current;
			_name = name;
			_cookie = new HttpCookie(_name);
			_cookie.Expires = DateTime.Now.AddDays(exprires);
		}

		public bool Exists()
		{
			if ((_cookie != null) && (_cookie.Value != null)) return true;
			return false;
		}

		public void Remove()
		{
			_cookie = _context.Request.Cookies[_name];
			if (this.Exists() == true)
			{
				_cookie.Expires = DateTime.Now.AddDays(-1);
				_context.Response.Cookies.Add(_cookie);
			}
		}

		public void SetCookie<T>(T value)
		{
			string temp = JsonConvert.SerializeObject(value);
			_cookie.Value = temp;
			_context.Response.Cookies.Add(_cookie);
		}

		public T GetCookie<T>()
		{
			_cookie = _context.Request.Cookies[_name];
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