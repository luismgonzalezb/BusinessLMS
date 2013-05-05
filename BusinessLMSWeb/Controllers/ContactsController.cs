using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{

	[Authorize]
	public class ContactsController : BaseWebController
	{

		private SelectList languages
		{
			get
			{
				return new SelectList(ControllersHelper.GetLanguages(baseApiUrl), "languageId", "language1");
			}
		}

		private SelectList contacttypes
		{
			get
			{
				return new SelectList(ControllersHelper.GetContactTypes(baseApiUrl, ibo.languageId), "contactTypeId", "type");
			}
		}

		public ActionResult Index()
		{
			BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetIBOContacts");
			List<Contact> contacts = client.Get<List<Contact>>(ibo.IBONum);
			return View(contacts);
		}

		[IsNotPageRefresh]
		public ActionResult NewContact()
		{
			ViewBag.languages = languages;
			ViewBag.contacttypes = contacttypes;
			Contact contact = new Contact();
			contact.IBONum = ibo.IBONum;
			contact.datetime = DateTime.Now;
			contact.birthday = DateTime.Now;
			return PartialView(contact);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult NewContactAjax(Contact model)
		{
			if (ModelState.IsValid == true)
			{
				BaseClient client = new BaseClient(baseApiUrl, "Contacts", "PostContact");
				bool result = client.Post<Contact>(model);
				if (result)
				{
					return Json(model);
				}
				else
				{
					return Json(new { success = false, message = "The contact already exists." });
				}
			}
			else
			{
				return Json(new { success = false, message = "Please correct all the issues." });
			}
		}

		[IsNotPageRefresh]
		public ActionResult EditContact(string id)
		{
			ViewBag.languages = languages;
			ViewBag.contacttypes = contacttypes;
			BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetContact");
			Contact contact = client.Get<Contact>(id);
			return PartialView(contact);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult EditContactAjax(Contact model)
		{
			if (ModelState.IsValid == true)
			{
				try
				{
					BaseClient client = new BaseClient(baseApiUrl, "Contacts", "PutContact");
					string result = client.Put<Contact>(model.contactId.ToString(), model);
					return Json(new { success = true });
				}
				catch
				{
					return Json(new { success = false, message = "There was an issue with the server, please try again latter." });
				}
			}
			else
			{
				return Json(new { success = false, message = "Please correct all the issues." });
			}
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult DeleteContactAjax(string id)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "Contacts", "DeleteContact");
				string result = client.Delete(id);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false, message = "There was an issue with the server, please try again latter." });
			}
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

	}
}
