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

        [Start.Web.Common.AdminAuthentication(Authentication = false)]
        public ActionResult Logout()
        {
            MaLiang.Web.Utils.WriteCookie(EnumConst.CookieAdmin, "", -1);
            return View("Index");
        }

        public ActionResult UsersList(string name, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex};
            UsersInfo usersInfo = new UsersInfo {Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult Users(int? id)
        {
            UsersInfo usersInfo = null;
            if (id != null && id.Value != 0) usersInfo = Start.DatabaseProvider.Instance().GetUserByID(id.Value);
            if (usersInfo == null) usersInfo = new UsersInfo ();
            return View(usersInfo);
        }

        [HttpPost]
        public ActionResult Users(UsersInfo usersInfo)
        {
            if (usersInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (string.IsNullOrEmpty(usersInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入登陆名称" });
            if (string.IsNullOrEmpty(usersInfo.Email)) return Json(new FeedbackInfo { Success = -1, Message = "请输入邮箱" });
            usersInfo.Phone = string.IsNullOrWhiteSpace(usersInfo.Phone) == true ? "" : usersInfo.Phone;
            usersInfo.Password = string.IsNullOrWhiteSpace(usersInfo.Password) == true ? "" : usersInfo.Password;
            usersInfo.RealName = string.IsNullOrWhiteSpace(usersInfo.RealName) == true ? "" : usersInfo.RealName;
            usersInfo.Photo = string.IsNullOrWhiteSpace(usersInfo.Photo) == true ? "" : usersInfo.Photo;
            //新建
            int result = 0;
            if (usersInfo.ID == 0)
            {
                usersInfo.Date = DateTime.Now;
                result = Start.DatabaseProvider.Instance().InsertUser(usersInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdateUser(usersInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }

        public ActionResult EmailProfile(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult Email(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult EmailAddress(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult EmailSend(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult MessageProfile(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult PhoneAddress(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult MessageSend(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }

        public ActionResult Help(string name, int pageIndex = 1, int pageSize = 20)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex, PageSize = pageSize };
            UsersInfo usersInfo = new UsersInfo { Name = name };
            IList<UsersInfo> usersInfos = Start.DatabaseProvider.Instance().GetUser(usersInfo, pageInfo);
            PagedList<UsersInfo> pagedList = new PagedList<UsersInfo>(usersInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.UsresInfo = usersInfo;
            return View(pagedList);
        }


    }
}
