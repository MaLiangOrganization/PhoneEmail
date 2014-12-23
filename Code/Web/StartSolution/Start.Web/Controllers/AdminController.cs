using System;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Webdiyer.WebControls.Mvc;
using Start.Entity;

namespace Start.Web.Controllers
{
    [Start.Web.Common.AdminAuthentication]
    public class AdminController : Controller
    {
        [Start.Web.Common.AdminAuthentication(Authentication = false)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Start.Web.Common.AdminAuthentication(Authentication = false)]
        public ActionResult UserLogin(string name, string password)
        {
            if (string.IsNullOrEmpty(name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入用户名" });
            if (string.IsNullOrEmpty(password)) return Json(new FeedbackInfo { Success = -1, Message = "请输入密码" });
            password = MaLiang.Common.Security.Security.MD5(password);
            UsersInfo usersInfo = Start.DatabaseProvider.Instance().GetUserByNameAndPassword(name, password);
            if (usersInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "用户名或密码错误" });
            else
            {
                if (usersInfo.UserGroup != Convert.ToInt16(EnumUserGroup.Admin)) return Json(new FeedbackInfo { Success = -1, Message = "你不是管理员，无法登陆" });
                usersInfo.Password = null;
                string usersJson = JsonConvert.SerializeObject(usersInfo);
                usersJson = MaLiang.Common.Security.Security.Encrypt(usersJson, EnumConst.PasswordKey);
                MaLiang.Web.Utils.WriteCookie(EnumConst.CookieAdmin, usersJson);
                return Json(new FeedbackInfo { Success = 1, Message = "登陆成功" });
            }
        }

        public ActionResult EmailList()
        {
            return View();
        }


    }
}
