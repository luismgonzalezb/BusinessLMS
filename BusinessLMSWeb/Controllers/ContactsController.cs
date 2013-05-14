using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
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
				return new SelectList(IBOVirtualAPI.GetLanguages(), "languageId", "language1");
			}
		}

		private SelectList contacttypes
		{
			get
			{
				return new SelectList(IBOVirtualAPI.GetContactTypes(ibo.languageId), "contactTypeId", "type");
			}
		}

		public ActionResult Index()
		{
			List<Contact> contacts = IBOVirtualAPI.GetIBOContacts(ibo.IBONum);
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
				bool result = IBOVirtualAPI.Create<Contact>(model);
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
			Contact contact = IBOVirtualAPI.Get<Contact>(id);
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
					string result = IBOVirtualAPI.Update<Contact>(model.contactId.ToString(), model);
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
				string result = IBOVirtualAPI.Delete<Contact>(id);
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
