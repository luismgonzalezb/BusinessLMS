using BusinessLMS.Models;
using BusinessLMSWeb.ModelsView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;

namespace BusinessLMSWeb.Helpers
{

	internal class Cookies
	{
		public IBOCookie iboCookie;
		public FIdCookie fidCookie;
		public ATCookie atCookie;
		public ContactsCookie contactsCookie;
		public MenuItemsCookie menuItemsCooke;
		public Cookies(HttpContextBase context)
		{
			iboCookie = new IBOCookie(context);
			fidCookie = new FIdCookie(context);
			atCookie = new ATCookie(context);
			contactsCookie = new ContactsCookie(context);
			menuItemsCooke = new MenuItemsCookie(context);
		}
		public void Nullify()
		{
			iboCookie.Nullify();
			fidCookie.Nullify();
			atCookie.Nullify();
			contactsCookie.Nullify();
			menuItemsCooke.Nullify();
		}
	}

	#region Cookie Objects

	internal class IBOCookie : CookieBase
	{
		public IBOCookie(HttpContextBase context) : base(context, "IBOinfo") { }
		public IBO GetIBO()
		{
			return this.Get<IBO>();
		}
		public void SetIBO(IBO data)
		{
			this.Set<IBO>(data);
		}
	}

	internal class FIdCookie : CookieBase
	{
		public FIdCookie(HttpContextBase context) : base(context, "IBOfacebookID") { }
		public string GetFacebookID()
		{
			return this.Get();
		}
		public void SetFacebookID(string data)
		{
			this.Set(data);
		}
	}

	internal class ATCookie : CookieBase
	{
		public ATCookie(HttpContextBase context) : base(context, "IBOAccessToken") { }
		public string GetAccessToken()
		{
			return this.Get();
		}
		public void SetAccessToken(string data)
		{
			this.Set(data);
		}
	}

	internal class MenuItemsCookie : CookieBase
	{
		public MenuItemsCookie(HttpContextBase context) : base(context, "MenuItemsCookie") { }
		public List<MenuItem> GetMenuItems()
		{
			return this.Get<List<MenuItem>>();
		}
		public void SetMenuItems(List<MenuItem> data)
		{
			this.Set<List<MenuItem>>(data);
		}
	}

	internal class ContactsCookie : CookieBase
	{
		public ContactsCookie(HttpContextBase context) : base(context, "Contacts") { }
		public List<Contact> GetContacts()
		{
			return this.Get<List<Contact>>();
		}
		public void SetContacts(List<Contact> data)
		{
			this.Set<List<Contact>>(data);
		}
	}

	internal class IBOSearchCookie : CookieBase
	{
		public IBOSearchCookie(HttpContextBase context) : base(context, "IBOSearchCookie") { }
		public List<SearchObject> GetIBOs()
		{
			return this.Get<List<SearchObject>>();
		}
		public void SetIBOs(List<SearchObject> data)
		{
			this.Set<List<SearchObject>>(data);
		}
	}

	#endregion

	#region Cookie Base Class

	public class CookieBase
	{

		private static double CookieExipirationMinutes { get { return double.Parse(ConfigurationManager.AppSettings["CookieExipirationMinutes"]); } }

		protected readonly string CookieName;
		protected HttpContextBase Context;

		internal CookieBase(HttpContextBase context, string CookieName)
		{
			Context = context;
			this.CookieName = CookieName;
		}

		public bool Exists(int i = 0)
		{
			return Context.Request.CookieExists(CookieName);
		}

		public bool Exists(Predicate<HttpCookie> pred)
		{
			HttpCookie cookie = Context.Request.Cookies.Get(CookieName);
			return cookie != null && pred(cookie);
		}

		public void Set(string data)
		{
			HttpCookie httpCookie = new HttpCookie(CookieName);
			httpCookie.Expires = DateTime.Now.AddMinutes(CookieExipirationMinutes);
			httpCookie.Path = "/";
			string encrypted = Encryption.Encrypt(data);
			httpCookie.Value = encrypted;
			Context.Response.Cookies.Set(httpCookie);
		}

		public void Set<T>(T data)
		{
			HttpCookie httpCookie = new HttpCookie(CookieName);
			httpCookie.Expires = DateTime.Now.AddMinutes(CookieExipirationMinutes);
			httpCookie.Path = "/";
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.NullValueHandling = NullValueHandling.Ignore;
			string value = JsonConvert.SerializeObject(data, Formatting.None, settings);
			string compressed = Compress(value);
			string encrypted = Encryption.Encrypt(compressed);
			httpCookie.Value = encrypted;
			Context.Response.Cookies.Set(httpCookie);
		}

		public string Get()
		{
			HttpCookie cookie = Context.Request.Cookies.Get(CookieName);
			string data;
			try
			{
				data = Encryption.Decrypt(cookie.Value);
			}
			catch
			{
				data = null;
			}
			return data;
		}

		public T Get<T>()
		{
			HttpCookie cookie = Context.Request.Cookies.Get(CookieName);
			T data;
			data = default(T);
			try
			{
				string cvalue = Encryption.Decrypt(cookie.Value);
				string value = Decompress(cvalue);
				data = JsonConvert.DeserializeObject<T>(value);
			}
			catch
			{
			}
			return data;
		}

		public virtual void Nullify()
		{
			HttpCookie cookie = new HttpCookie(CookieName, "CLEAR");
			cookie.Path = "/";
			cookie.Expires = DateTime.UtcNow.AddDays(-1);
			Context.Response.Cookies.Set(cookie);
		}

		public static string Compress(string text)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(text);
			MemoryStream ms = new MemoryStream();
			using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
			{
				zip.Write(buffer, 0, buffer.Length);
			}
			ms.Position = 0;
			MemoryStream outStream = new MemoryStream();
			byte[] compressed = new byte[ms.Length];
			ms.Read(compressed, 0, compressed.Length);
			byte[] gzBuffer = new byte[compressed.Length + 4];
			System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
			System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
			return Convert.ToBase64String(gzBuffer);
		}

		public static string Decompress(string compressedText)
		{
			byte[] gzBuffer = Convert.FromBase64String(compressedText);
			using (MemoryStream ms = new MemoryStream())
			{
				int msgLength = BitConverter.ToInt32(gzBuffer, 0);
				ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
				byte[] buffer = new byte[msgLength];
				ms.Position = 0;
				using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
				{
					zip.Read(buffer, 0, buffer.Length);
				}
				return Encoding.UTF8.GetString(buffer);
			}
		}

	}

	#endregion

	#region Cookie Extension

	public static class Cookie
	{

		private static double CookieExipirationMinutes { get { return double.Parse(ConfigurationManager.AppSettings["CookieExipirationMinutes"]); } }

		public static void AddCookie(this HttpResponseBase response, string key, string value)
		{
			AddCookie(response, key, value, DateTime.UtcNow.AddMinutes(CookieExipirationMinutes));
		}

		public static void AddCookie(this HttpResponseBase response, string key, List<KeyValuePair<string, string>> values)
		{
			AddCookie(response, key, values, DateTime.UtcNow.AddMinutes(CookieExipirationMinutes));
		}

		public static void AddCookie(this HttpResponseBase response, string key, string value, DateTime expirationDateUTC)
		{
			response.Cookies.Add(CreateCookie(response, key, value, expirationDateUTC));
		}

		public static void AddCookie(this HttpResponseBase response, string key, List<KeyValuePair<string, string>> values, DateTime expirationDateUTC)
		{
			response.Cookies.Add(CreateCookie(response, key, values, expirationDateUTC));
		}

		public static void AddCookie(this HttpResponseBase response, HttpCookie cookie)
		{
			response.Cookies.Add(cookie);
		}

		public static HttpCookie CreateCookie(this HttpResponseBase response, string key, string value)
		{
			return CreateCookie(response, key, value, DateTime.UtcNow.AddMinutes(CookieExipirationMinutes));
		}

		public static HttpCookie CreateCookie(this HttpResponseBase response, string key, List<KeyValuePair<string, string>> values)
		{
			return CreateCookie(response, key, values, DateTime.UtcNow.AddMinutes(CookieExipirationMinutes));
		}

		public static HttpCookie CreateCookie(this HttpResponseBase response, string key, string value, DateTime expirationDateUTC)
		{
			HttpCookie cookie = new HttpCookie(key, value);
			cookie.Expires = expirationDateUTC;
			return cookie;
		}

		public static HttpCookie CreateCookie(this HttpResponseBase response, string key, List<KeyValuePair<string, string>> values, DateTime expirationDateUTC)
		{
			HttpCookie cookie = new HttpCookie(key);
			cookie.Expires = expirationDateUTC;
			for (int i = 0; i < values.Count; i++)
			{
				cookie.Values.Add(values[i].Key, values[i].Value);
			}
			return cookie;
		}

		public static void UpdateCookie(this HttpResponseBase response, string key, string value)
		{
			AddCookie(response, key, value);
		}

		public static void UpdateCookie(this HttpResponseBase response, string key, string value, DateTime expirationDateUTC)
		{
			AddCookie(response, key, value, expirationDateUTC);
		}

		public static void UpdateCookie(this HttpResponseBase response, string key, List<KeyValuePair<string, string>> values)
		{
			AddCookie(response, key, values);
		}

		public static void UpdateCookie(this HttpResponseBase response, string key, List<KeyValuePair<string, string>> values, DateTime expirationDateUTC)
		{
			AddCookie(response, key, values, expirationDateUTC);
		}

		public static void UpdateCookie(this HttpResponseBase response, HttpCookie cookie)
		{
			AddCookie(response, cookie);
		}

		public static bool CookieExists(this HttpRequestBase request, string key)
		{
			return request.Cookies.Get(key) != null ? true : false;
		}

		public static bool CookieExistsPlusValue(this HttpRequestBase request, string key, string value)
		{
			HttpCookie cookie = request.Cookies.Get(key);
			if (cookie != null && cookie.Value == value)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static string GetCookieValue(this HttpRequestBase request, string key)
		{
			HttpCookie obj = request.Cookies.Get(key);
			if (obj == null) { return null; }
			return obj.Value.ToString();
		}

		public static List<KeyValuePair<string, string>> GetCookieValues(this HttpRequestBase request, string key)
		{
			HttpCookie obj = request.Cookies.Get(key);
			if (obj == null) { return null; }
			List<KeyValuePair<string, string>> kvps = new List<KeyValuePair<string, string>>();
			for (int i = 0; i < obj.Values.Count; i++)
			{
				kvps.Add(new KeyValuePair<string, string>(obj.Values.Keys[i], obj.Values.Keys[i]));
			}
			return kvps;
		}
	}

	#endregion

}