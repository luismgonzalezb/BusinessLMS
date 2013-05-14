using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	public class BooksController : BaseWebController
	{
		public ActionResult Index()
		{
			List<Book> Books = IBOVirtualAPI.GetIBOBooks(ibo.IBONum);
			List<Book> CBooks = IBOVirtualAPI.GetMyBooks(ibo.IBONum);
			ViewBag.ibolevel = ibo.level;
			ViewBag.BookCount = CBooks.Count;
			return View(Books);
		}

		[IsNotPageRefresh]
		public ActionResult CreateBook()
		{
			List<Book> BookList = IBOVirtualAPI.GetBooks();
			ViewBag.BookList = BookList;
			Book Book = new Book();
			Book.IBONum = ibo.IBONum;
			return PartialView(Book);
		}

		[HttpPost]
		public ActionResult CreateBook(Book model)
		{
			bool result = IBOVirtualAPI.Create<Book>(model);
			return RedirectToAction("Index");
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult CreateBookAjax(Book model)
		{
			if (ModelState.IsValid == true)
			{
				bool result = IBOVirtualAPI.Create<Book>(model);
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
				string result = IBOVirtualAPI.Delete<Book>(id);
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
			Book Book = IBOVirtualAPI.Get<Book>(id);
			Book.Count += 1;
			string result = IBOVirtualAPI.Update<Book>(Book.BookId.ToString(), Book);
			return Json(new { success = true });
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

	}
}
