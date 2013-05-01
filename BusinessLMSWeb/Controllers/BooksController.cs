using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	public class BooksController : BaseWebController
	{
		public ActionResult Index()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "GetIBOBooks");
			List<Book> Books = client.Get<List<Book>>(ibo.IBONum);
			ViewBag.ibolevel = ibo.level;
			client = new BaseClient(baseApiUrl, "Books", "GetMyBook");
			List<Book> CBooks = client.Get<List<Book>>(ibo.IBONum);
			ViewBag.BookCount = CBooks.Count;
			return View(Books);
		}

		[IsNotPageRefresh]
		public ActionResult CreateBook()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "GetBooks");
			List<Book> BookList = client.Get<List<Book>>();
			ViewBag.BookList = BookList;
			Book Book = new Book();
			Book.IBONum = ibo.IBONum;
			return PartialView(Book);
		}

		[HttpPost]
		public ActionResult CreateBook(Book model)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "PostBook");
			bool result = client.Post<Book>(model);
			return RedirectToAction("Index");
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult CreateBookAjax(Book model)
		{
			if (ModelState.IsValid == true)
			{
				BaseClient client = new BaseClient(baseApiUrl, "Books", "PostBook");
				bool result = client.Post<Book>(model);
				if (result)
				{
					return Json(model);
				}
				else
				{
					return Json(new { success = false });
				}
			}
			else
			{
				return Json(new { success = false });
			}
		}

		[IsNotPageRefresh]
		public ActionResult DeleteBookAjax(string id)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Books", "DeleteBook");
				string result = client.Delete(id);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		[IsNotPageRefresh]
		public ActionResult UpCount(string id)
		{
			BaseClient client = new BaseClient(baseApiUrl, "Books", "GetBook");
			Book Book = client.Get<Book>(id);
			Book.Count += 1;
			client = new BaseClient(baseApiUrl, "Books", "PutBook");
			string result = client.Put<Book>(Book.BookId.ToString(), Book);
			return Json(new { success = true });
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

	}
}
