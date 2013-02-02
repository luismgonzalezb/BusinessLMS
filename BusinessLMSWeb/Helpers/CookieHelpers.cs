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

		public CookieHelper(HttpContext current)
		{
			_context = current;
		}

		public CookieHelper(HttpContext current, string name, double exprires = 1)
		{
			_context = current;
			_name = name;
			_cookie = new HttpCookie(_name);
			if (_context.Request.Cookies[_name] != null)
			{
				_cookie = _context.Request.Cookies[_name];
			}
			_cookie.Expires = DateTime.Now.AddMinutes(exprires);
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
			string temp = HttpUtility.UrlEncode(JsonConvert.SerializeObject(value));
			_cookie.Value = temp;
			_context.Response.Cookies.Add(_cookie);
		}

		public T GetCookie<T>()
		{
			if (this.Exists() == true)
			{
				string temp = HttpUtility.UrlDecode(_cookie.Value);
				return JsonConvert.DeserializeObject<T>(temp);
			}
			else
			{
				return default(T);
			}
		}

		public bool CleanCookies()
		{
			try
			{
				int cookieCount = _context.Request.Cookies.Count;
				for (int i = 0; i < cookieCount; i++)
				{
					HttpCookie cookie = _context.Request.Cookies[i];
					if ((cookie.Name != "__RequestVerificationToken") && (cookie.Name != ".ASPXAUTH"))
					{
						cookie.Expires = DateTime.Now.AddDays(-1);
						_context.Response.Cookies.Add(cookie);
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}