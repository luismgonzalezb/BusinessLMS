using System.Web.Http;
using BusinessLMS.ActionFilters;
using BusinessLMS.Helpers;

namespace BusinessLMS.Controllers
{
    [BasicAuthentication]
    public class EmailController : ApiController
    {
        public bool PostResetEmail(ResertEmailContact contact)
        {
            EmailHelper email = new EmailHelper();
            return email.SendEmail(contact.name,contact.email,contact.token,EmailHelper.EmailType.resetEmail);
        }
    }
}
