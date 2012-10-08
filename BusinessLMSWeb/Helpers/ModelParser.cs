using System.Web;
using BusinessLMSWeb.Models;

namespace BusinessLMSWeb.Helpers
{
    public static class ModelParser
    {
        public static IBO ParseIBO(IBO model)
        {
            IBO ibo = new IBO();
            ibo.IBONum = model.IBONum;
            ibo.firstName = model.firstName;
            ibo.lastName = model.lastName;
            ibo.languageId = model.languageId;
            ibo.email = model.email;
            ibo.picture = HttpUtility.HtmlEncode(model.picture != null ? model.picture : "");
            ibo.twitter = model.twitter != null ? model.twitter : "";
            ibo.UPLine = model.UPLine != null ? model.UPLine : "";
            return ibo;
        }
    }
}