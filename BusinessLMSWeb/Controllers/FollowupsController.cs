using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;
using BusinessLMSWeb.ModelsView;
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
			get
			{
				return Cookies.contactsCookie.GetContacts();
			}
			set
			{
				if (value != null) Cookies.contactsCookie.SetContacts(value); else Cookies.contactsCookie.Nullify();
			}
		}

		public ActionResult Index()
		{
			/* Get Clients For Autocomplete */
			_contacts = null;
			BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetIBOContacts");
			List<Contact> contacts = client.Get<List<Contact>>(ibo.IBONum);
			_contacts = contacts;

			client = new BaseClient(baseApiUrl, "ContactFollowup", "GetIBOFollowupView");
			List<FollowupView> followups = client.Get<List<FollowupView>>(ibo.IBONum);
			return View(followups);
		}

		public ActionResult NewFollowup()
		{
			ContactFollowup model = new ContactFollowup();
			model.IBONum = ibo.IBONum;
			model.completed = false;
			model.datetime = System.DateTime.Now;
			return PartialView(model);
		}

		[HttpPost]
		public ActionResult NewFollowupAjax(ContactFollowup model)
		{
			if (ModelState.IsValid == true && (model.contactId != 0))
			{
				try
				{
					BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "PostContactFollowup");
					string result = client.Post<ContactFollowup>(model);
					return Json(new { success = true });
				}
				catch
				{
					return Json(new { success = false, message = "There Was an issue saving the Followup, please try again. " });
				}
			}
			else
			{
				return Json(new { success = false, message = "Selecting a Contact is required" });
			}
		}

		public ActionResult EditFollowup(int id)
		{
			BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "GetContactFollowup");
			ContactFollowup followup = client.Get<ContactFollowup>(id.ToString());
			client = new BaseClient(baseApiUrl, "Contacts", "GetContact");
			Contact contact = client.Get<Contact>(followup.contactId.ToString());
			ViewBag.contactName = contact.GetFullName();
			return PartialView(followup);
		}

		[HttpPost]
		public ActionResult EditFollowupAjax(ContactFollowup model)
		{
			if (ModelState.IsValid == true)
			{
				BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "PutContactFollowup");
				string result = client.Put<ContactFollowup>(model.followupId.ToString(), model);
				return Json(new { success = true });
			}
			else
			{
				return Json(new { success = false });
			}
		}

		[HttpPost]
		public ActionResult DeleteFollowupAjax(int id)
		{
			try
			{
				BaseClient client = new BaseClient(baseApiUrl, "ContactFollowup", "DeleteContactFollowup");
				string result = client.Delete(id.ToString());
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
		}

		[HttpGet]
		public ActionResult SearchContact(string term)
		{
			List<SearchObject> userNames = (from cnt in _contacts
											where cnt.firstName.ToUpper().Contains(term.ToUpper()) || cnt.lastName.ToUpper().Contains(term.ToUpper())
											select new SearchObject { label = cnt.GetFullName(), value = cnt.contactId.ToString() }).ToList();
			return Json(userNames, JsonRequestBehavior.AllowGet);
		}

		public ActionResult _HelpInfo()
		{
			return PartialView();
		}

	}
}
