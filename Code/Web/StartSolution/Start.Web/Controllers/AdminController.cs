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
            if (string.IsNullOrWhiteSpace(usersInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入登陆名称" });
            if (string.IsNullOrWhiteSpace(usersInfo.Email)) return Json(new FeedbackInfo { Success = -1, Message = "请输入邮箱" });
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



        public ActionResult EmailProfileList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            EmailProfileInfo emailProfileInfo = new EmailProfileInfo { UserID = userID };
            IList<EmailProfileInfo> emailProfileInfos = Start.DatabaseProvider.Instance().GetEmailProfile(emailProfileInfo, pageInfo);
            PagedList<EmailProfileInfo> pagedList = new PagedList<EmailProfileInfo>(emailProfileInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.EmailProfileInfo = emailProfileInfo;
            return View(pagedList);
        }

        public ActionResult EmailProfile(int? id)
        {
            EmailProfileInfo emailProfileInfo = null;
            if (id != null && id.Value != 0) emailProfileInfo = Start.DatabaseProvider.Instance().GetEmailProfileByID(id.Value);
            if (emailProfileInfo == null) emailProfileInfo = new EmailProfileInfo();
            return View(emailProfileInfo);
        }

        [HttpPost]
        public ActionResult EmailProfile(EmailProfileInfo emailProfileInfo)
        {
            if (emailProfileInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (emailProfileInfo.UserID==0) return Json(new FeedbackInfo { Success = -1, Message = "请输入用户编号" });
            if (string.IsNullOrWhiteSpace(emailProfileInfo.SMTP)) return Json(new FeedbackInfo { Success = -1, Message = "请输入发送邮箱服务器" });
            if (string.IsNullOrWhiteSpace(emailProfileInfo.SMTPPort)) return Json(new FeedbackInfo { Success = -1, Message = "请输入发送邮箱服务器端口" });
            if (string.IsNullOrWhiteSpace(emailProfileInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入邮箱登陆名" });
            if (string.IsNullOrWhiteSpace(emailProfileInfo.Password)) return Json(new FeedbackInfo { Success = -1, Message = "请输入邮箱登陆密码" });
            //新建
            int result = 0;
            if (emailProfileInfo.ID == 0)
            {
                result = Start.DatabaseProvider.Instance().InsertEmailProfile(emailProfileInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdateEmailProfile(emailProfileInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }



        public ActionResult EmailList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            EmailInfo emailInfo = new EmailInfo { UserID = userID };
            IList<EmailInfo> emailInfos = Start.DatabaseProvider.Instance().GetEmail(emailInfo, pageInfo);
            PagedList<EmailInfo> pagedList = new PagedList<EmailInfo>(emailInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.EmailInfo = emailInfo;
            return View(pagedList);
        }

        public ActionResult Email(int? id)
        {
            EmailInfo emailInfo = null;
            if (id != null && id.Value != 0) emailInfo = Start.DatabaseProvider.Instance().GetEmailByID(id.Value);
            if (emailInfo == null) emailInfo = new EmailInfo();
            return View(emailInfo);
        }

        [HttpPost]
        public ActionResult Email(EmailInfo emailInfo)
        {
            if (emailInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (emailInfo.UserID == 0) return Json(new FeedbackInfo { Success = -1, Message = "请输入用户编号" });
            if (string.IsNullOrWhiteSpace(emailInfo.Subject)) return Json(new FeedbackInfo { Success = -1, Message = "请输入主题" });
            if (string.IsNullOrWhiteSpace(emailInfo.Memo)) return Json(new FeedbackInfo { Success = -1, Message = "请输入内容" });
            //新建
            int result = 0;
            if (emailInfo.ID == 0)
            {
                result = Start.DatabaseProvider.Instance().InsertEmail(emailInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdateEmail(emailInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }



        public ActionResult EmailAddressList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            EmailAddressInfo emailAddressInfo = new EmailAddressInfo { UserID = userID };
            IList<EmailAddressInfo> emailAddressInfos = Start.DatabaseProvider.Instance().GetEmailAddress(emailAddressInfo, pageInfo);
            PagedList<EmailAddressInfo> pagedList = new PagedList<EmailAddressInfo>(emailAddressInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.EmailAddressInfo = emailAddressInfo;
            return View(pagedList);
        }

        public ActionResult EmailAddress(int? id)
        {
            EmailAddressInfo emailAddressInfo = null;
            if (id != null && id.Value != 0) emailAddressInfo = Start.DatabaseProvider.Instance().GetEmailAddressByID(id.Value);
            if (emailAddressInfo == null) emailAddressInfo = new EmailAddressInfo();
            return View(emailAddressInfo);
        }

        [HttpPost]
        public ActionResult EmailAddress(EmailAddressInfo emailAddressInfo)
        {
            if (emailAddressInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (emailAddressInfo.UserID == 0) return Json(new FeedbackInfo { Success = -1, Message = "请输入用户编号" });
            if (string.IsNullOrWhiteSpace(emailAddressInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入名称" });
            if (string.IsNullOrWhiteSpace(emailAddressInfo.Email)) return Json(new FeedbackInfo { Success = -1, Message = "请输入邮箱" });
            //新建
            int result = 0;
            if (emailAddressInfo.ID == 0)
            {
                result = Start.DatabaseProvider.Instance().InsertEmailAddress(emailAddressInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdateEmailAddress(emailAddressInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }



        public ActionResult EmailSendList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            EmailSendInfo emailSendInfo = new EmailSendInfo { UserID = userID };
            IList<EmailSendInfo> emailSendInfos = Start.DatabaseProvider.Instance().GetEmailSend(emailSendInfo, pageInfo);
            PagedList<EmailSendInfo> pagedList = new PagedList<EmailSendInfo>(emailSendInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.EmailSendInfo = emailSendInfo;
            return View(pagedList);
        }



        public ActionResult MessageProfileList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            MessageProfileInfo messageProfileInfo = new MessageProfileInfo { UserID = userID };
            IList<MessageProfileInfo> messageProfileInfos = Start.DatabaseProvider.Instance().GetMessageProfile(messageProfileInfo, pageInfo);
            PagedList<MessageProfileInfo> pagedList = new PagedList<MessageProfileInfo>(messageProfileInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.MessageProfileInfo = messageProfileInfo;
            return View(pagedList);
        }

        public ActionResult MessageProfile(int? id)
        {
            MessageProfileInfo messageProfileInfo = null;
            if (id != null && id.Value != 0) messageProfileInfo = Start.DatabaseProvider.Instance().GetMessageProfileByID(id.Value);
            if (messageProfileInfo == null) messageProfileInfo = new MessageProfileInfo();
            return View(messageProfileInfo);
        }

        [HttpPost]
        public ActionResult MessageProfile(MessageProfileInfo messageProfileInfo)
        {
            if (messageProfileInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (messageProfileInfo.UserID == 0) return Json(new FeedbackInfo { Success = -1, Message = "请输入用户编号" });
            if (string.IsNullOrWhiteSpace(messageProfileInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入短信名称" });
            if (string.IsNullOrWhiteSpace(messageProfileInfo.Memo)) return Json(new FeedbackInfo { Success = -1, Message = "请输入短信内容" });
            //新建
            int result = 0;
            if (messageProfileInfo.ID == 0)
            {
                result = Start.DatabaseProvider.Instance().InsertMessageProfile(messageProfileInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdateMessageProfile(messageProfileInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }



        public ActionResult PhoneAddressList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            PhoneAddressInfo phoneAddressInfo = new PhoneAddressInfo { UserID = userID };
            IList<PhoneAddressInfo> phoneAddressInfos = Start.DatabaseProvider.Instance().GetPhoneAddress(phoneAddressInfo, pageInfo);
            PagedList<PhoneAddressInfo> pagedList = new PagedList<PhoneAddressInfo>(phoneAddressInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.PhoneAddressInfo = phoneAddressInfo;
            return View(pagedList);
        }

        public ActionResult PhoneAddress(int? id)
        {
            PhoneAddressInfo phoneAddressInfo = null;
            if (id != null && id.Value != 0) phoneAddressInfo = Start.DatabaseProvider.Instance().GetPhoneAddressByID(id.Value);
            if (phoneAddressInfo == null) phoneAddressInfo = new PhoneAddressInfo();
            return View(phoneAddressInfo);
        }

        [HttpPost]
        public ActionResult PhoneAddress(PhoneAddressInfo phoneAddressInfo)
        {
            if (phoneAddressInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (phoneAddressInfo.UserID == 0) return Json(new FeedbackInfo { Success = -1, Message = "请输入用户编号" });
            if (string.IsNullOrWhiteSpace(phoneAddressInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入联系人名" });
            if (string.IsNullOrWhiteSpace(phoneAddressInfo.Phone)) return Json(new FeedbackInfo { Success = -1, Message = "请输入联系人手机" });
            //新建
            int result = 0;
            if (phoneAddressInfo.ID == 0)
            {
                result = Start.DatabaseProvider.Instance().InsertPhoneAddress(phoneAddressInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdatePhoneAddress(phoneAddressInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }



        public ActionResult MessageSendList(int userID = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            MessageSendInfo messageSendInfo = new MessageSendInfo { UserID = userID };
            IList<MessageSendInfo> messageSendInfos = Start.DatabaseProvider.Instance().GetMessageSend(messageSendInfo, pageInfo);
            PagedList<MessageSendInfo> pagedList = new PagedList<MessageSendInfo>(messageSendInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.MessageSendInfo = messageSendInfo;
            return View(pagedList);
        }



        public ActionResult HelpList(int id = 0, int pageIndex = 1)
        {
            PageInfo pageInfo = new PageInfo { PageIndex = pageIndex };
            HelpInfo helpInfo = new HelpInfo { ID = id };
            IList<HelpInfo> helpInfos = Start.DatabaseProvider.Instance().GetHelp(helpInfo, pageInfo);
            PagedList<HelpInfo> pagedList = new PagedList<HelpInfo>(helpInfos, pageInfo.PageIndex, pageInfo.PageSize, pageInfo.TotalRecord);
            ViewBag.HelpInfo = helpInfo;
            return View(pagedList);
        }

        public ActionResult Help(int? id)
        {
            HelpInfo helpInfo = null;
            if (id != null && id.Value != 0) helpInfo = Start.DatabaseProvider.Instance().GetHelpByID(id.Value);
            if (helpInfo == null) helpInfo = new HelpInfo();
            return View(helpInfo);
        }

        [HttpPost]
        public ActionResult Help(HelpInfo helpInfo)
        {
            if (helpInfo == null) return Json(new FeedbackInfo { Success = -1, Message = "获取实体出错" });
            if (string.IsNullOrWhiteSpace(helpInfo.Name)) return Json(new FeedbackInfo { Success = -1, Message = "请输入名称" });
            if (string.IsNullOrWhiteSpace(helpInfo.Memo)) return Json(new FeedbackInfo { Success = -1, Message = "请输入内容" });
            //新建
            int result = 0;
            if (helpInfo.ID == 0)
            {
                helpInfo.Date = DateTime.Now;
                result = Start.DatabaseProvider.Instance().InsertHelp(helpInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "添加成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "添加失败" });
            }
            else //编辑
            {
                result = Start.DatabaseProvider.Instance().UpdateHelp(helpInfo);
                if (result == 1)
                    return Json(new FeedbackInfo { Success = 1, Message = "修改成功" });
                else
                    return Json(new FeedbackInfo { Success = -1, Message = "修改失败" });
            }

        }


    }
}
