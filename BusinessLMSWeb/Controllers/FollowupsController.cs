using BusinessLMS.Models;
using BusinessLMSWeb.Filters;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusinessLMSWeb.Controllers
{
	[Authorize]
	public class FollowupsController : BaseWebController
	{
		private List<Contact> _contacts
		{
			get { return Cookies.contactsCookie.GetContacts(); }
			set { if (value != null) Cookies.contactsCookie.SetContacts(value); else Cookies.contactsCookie.Nullify(); }
		}

		public ActionResult Index()
		{
			/* Get Clients For Autocomplete */
			_contacts = null;
			_contacts = GetContacts();
			List<FollowupView> followups = IBOVirtualAPI.GetIBOFollowupView(ibo.IBONum);
			return View(followups);
		}

		[IsNotPageRefresh]
		public ActionResult NewFollowup()
		{
			ContactFollowup model = new ContactFollowup();
			model.IBONum = ibo.IBONum;
			model.completed = false;
			model.datetime = System.DateTime.Now;
			return PartialView(model);
		}

		[IsNotPageRefresh]
		public ActionResult NewFollowupDate(DateTime date)
		{
			ContactFollowup model = new ContactFollowup();
			model.IBONum = ibo.IBONum;
			model.completed = false;
			model.datetime = System.DateTime.Now;
			model.datetime = date;
			return PartialView("NewFollowup", model);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult NewFollowupAjax(ContactFollowup model)
		{
			if (ModelState.IsValid == true && (model.contactId != 0))
			{
				bool result = IBOVirtualAPI.Create<ContactFollowup>(model);
				if (result)
				{
					return Json(new { success = true });
				}
				else
				{
					return Json(new { success = false, message = "There Was an issue saving the Followup, please try again. " });
				}
			}
			else
			{
				return Json(new { success = false, message = "Selecting a Contact is required" });
			}
		}

		[IsNotPageRefresh]
		public ActionResult EditFollowup(int id)
		{
			ContactFollowup followup = IBOVirtualAPI.Get<ContactFollowup>(id.ToString());
			Contact contact = IBOVirtualAPI.Get<Contact>(followup.contactId.ToString());
			ViewBag.contactName = contact.GetFullName();
			return PartialView(followup);
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult EditFollowupAjax(ContactFollowup model)
		{
			if (ModelState.IsValid == true)
			{
				string result = IBOVirtualAPI.Update<ContactFollowup>(model.followupId.ToString(), model);
				return Json(new { success = true });
			}
			else
			{
				return Json(new { success = false });
			}
		}

		[HttpPost]
		[IsNotPageRefresh]
		public ActionResult DeleteFollowupAjax(int id)
		{
			try
			{
				string result = IBOVirtualAPI.Delete<ContactFollowup>(id.ToString());
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		[HttpGet]
		[IsNotPageRefresh]
		public ActionResult Details(int id)
		{
			ContactFollowup followup = IBOVirtualAPI.Get<ContactFollowup>(id.ToString());
			Contact contact = IBOVirtualAPI.Get<Contact>(followup.contactId.ToString());
			ViewBag.contactName = contact.GetFullName();
			return PartialView(followup);
		}

		[HttpGet]
		[IsNotPageRefresh]
		public ActionResult SearchContact(string term)
		{
			if (_contacts == null) _contacts = GetContacts();
			List<SearchObject> userNames = (from cnt in _contacts
											where cnt.firstName.ToUpper().Contains(term.ToUpper()) || cnt.lastName.ToUpper().Contains(term.ToUpper())
											select new SearchObject { label = cnt.GetFullName(), value = cnt.contactId.ToString() }).ToList();
			return Json(userNames, JsonRequestBehavior.AllowGet);
		}

		private List<Contact> GetContacts()
		{
			List<Contact> contacts = IBOVirtualAPI.GetIBOContacts(ibo.IBONum);
			return contacts;
		}

		[IsNotPageRefresh]
		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

		public ActionResult CompletFollowup (int id)
		{
			try
			{
				ContactFollowup followup = IBOVirtualAPI.Get<ContactFollowup>(id.ToString());
				followup.completed = true;
				string result = IBOVirtualAPI.Update<ContactFollowup>(followup.followupId.ToString(), followup);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}
	}
}