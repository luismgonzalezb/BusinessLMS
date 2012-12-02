using System;
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
            ibo.phone = model.phone;
            ibo.birthday = model.birthday != null ? model.birthday : DateTime.Now;
            ibo.picture = HttpUtility.HtmlEncode(model.picture != null ? model.picture : "");
            ibo.twitter = model.twitter != null ? model.twitter : "";
            ibo.UPLine = model.UPLine != null ? model.UPLine : "";
            return ibo;
        }

        public static Dream ParseDream(Dream model)
        {
            Dream newObject = new Dream();
            newObject.IBONum = model.IBONum;
            newObject.dream1 = model.dream1;
            newObject.achieved = model.achieved;
            newObject.areaId = model.areaId;
            newObject.datetime = model.datetime;
            newObject.picture = model.picture;
            newObject.dreamLevel = model.dreamLevel + 1;
            newObject.timeframeId = model.timeframeId + 1;
            return newObject;
        }

        public static Goal ParseGoal(Goal model)
        {
            Goal newObject = new Goal();
            newObject.dreamId = model.dreamId;
            newObject.completed = model.completed;
            newObject.goal1 = model.goal1;
            newObject.toolId = model.toolId;
            newObject.datetime = model.datetime;
            newObject.picture = model.picture;
            newObject.goalLevel = model.goalLevel + 1;
            newObject.timeframeId = model.timeframeId + 1;
            return newObject;
        }

    }
}