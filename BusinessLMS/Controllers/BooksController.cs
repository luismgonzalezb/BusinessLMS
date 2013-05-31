using BusinessLMS.ActionFilters;
using BusinessLMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLMS.Controllers
{
	[BasicAuthentication]
	public class BooksController : ApiController
	{
		private BusinessLMSContext db = new BusinessLMSContext();
		private List<Book> BooksIBO;

		// GET api/Books
		public IEnumerable<Book> GetBooks()
		{
			return db.Books.AsEnumerable();
		}

		// GET api/Books/5
		public Book GetBook(int id)
		{
			Book book = db.Books.Find(id);
			if (book == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return book;
		}

		public IEnumerable<Book> GetIBOBooks(string id)
		{
			BooksIBO = (from b in db.Books
						where b.IBONum == id
						select b).ToList();
			BooksIBO = GetRecursiveBooks(id).ToList();
			if (BooksIBO.Count() > 10)
			{
				BooksIBO.RemoveRange(10, BooksIBO.Count() - 10);
			}
			return GetRecursiveBooks(id);
		}

		private IEnumerable<Book> GetRecursiveBooks(string id)
		{
			if (BooksIBO.Count() < 10)
			{
				id = (from x in db.IBOs
					  where x.IBONum == id
					  select x.UPLine).FirstOrDefault();
				if (id != null && id != "")
				{
					BooksIBO.AddRange((from b in db.Books
									   where b.IBONum == id
									   select b).ToList());
					return GetRecursiveBooks(id);
				}
				else
				{
					return BooksIBO;
				}
			}
			else
			{
				return BooksIBO;
			}
		}
		public IEnumerable<Book> GetMyBook(string id)
		{
			return (from b in db.Books where b.IBONum == id select b);
		}

		// PUT api/Books/5
		public HttpResponseMessage PutBook(int id, Book book)
		{
			if (ModelState.IsValid && id == book.BookId)
			{
				db.Entry(book).State = EntityState.Modified;

				try
				{
					db.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}

				return Request.CreateResponse(HttpStatusCode.OK);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		// POST api/Books
		public HttpResponseMessage PostBook(Book book)
		{
			if (ModelState.IsValid)
			{
				db.Books.Add(book);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, book);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = book.BookId }));
				return response;
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
		}

		// DELETE api/Books/5
		public HttpResponseMessage DeleteBook(int id)
		{
			Book book = db.Books.Find(id);
			if (book == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Books.Remove(book);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, book);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}