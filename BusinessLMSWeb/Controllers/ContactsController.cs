using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLMSWeb.Helpers;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Controllers
{

    [Authorize]
    public class ContactsController : BaseWebController
    {

        public ActionResult Index()
        {
            BaseClient client = new BaseClient(baseApiUrl, "Contacts", "GetIBOContacts");
            List<Contact> contacts = client.Get<List<Contact>>(ibo.IBONum);
            return View(contacts);
        }

    }
}
