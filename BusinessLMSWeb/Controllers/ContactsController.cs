using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;

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
                return new SelectList(ControllersHelper.GetContactTypes(baseApiUrl), "contactTypeId", "type");
            }
        }

        public ActionResult Index()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetIBOContacts");
            List<Contact> contacts = client.Get<List<Contact>>(ibo.IBONum);
            return View(contacts);
        }

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
        public ActionResult NewContactAjax(Contact model)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    BaseClient client = new BaseClient(baseApiUrl, "Contacts", "PostContact");
                    string result = client.Post<Contact>(model);
                    ContactFollowup followup = new ContactFollowup();
                    followup.contactId = int.Parse(result);
                    followup.IBONum = ibo.IBONum;
                    followup.method = model.preferred;
                    followup.datetime = DateTime.Now.AddDays(1);
                    followup.completed = false;
                    client = new BaseClient(baseApiUrl, "ContactFollowup", "PostContactFollowup");
                    result = client.Post<ContactFollowup>(followup);
                    return Json(model);
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult EditContact(string id)
        {
            ViewBag.languages = languages;
            ViewBag.contacttypes = contacttypes;
            BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetContact");
            Contact contact = client.Get<Contact>(id);
            return PartialView(contact);
        }

        [HttpPost]
        public ActionResult EditContactAjax(Contact model)
        {
            if (ModelState.IsValid == true)
            {
                BaseClient client = new BaseClient(baseApiUrl, "Contacts", "PutContact");
                string result = client.Put<Contact>(model.contactId.ToString(), model);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

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
                return Json(new { success = false });
            }
        }

    }
}
