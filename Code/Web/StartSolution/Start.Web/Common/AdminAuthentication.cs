using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Start.Entity;

namespace Start.Web.Common
{
    public class AdminAuthentication : ActionFilterAttribute
    {
        public bool Authentication = true;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Authentication == false) return;
            UsersInfo usersInfo = Start.Web.Common.Parameter.LoginUser();
            if (usersInfo == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Index", false);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}