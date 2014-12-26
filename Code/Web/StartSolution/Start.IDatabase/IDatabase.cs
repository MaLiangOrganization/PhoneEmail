using System;
using Start.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Start
{
    public interface IDatabase
    {
        /************************************************* 用户 Users *************************************************/
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        int InsertUser(UsersInfo userInfo);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UsersInfo GetUserByNameAndPassword(string name, string password);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="ID">用户编号</param>
        UsersInfo GetUserByID(int ID);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<UsersInfo> GetUser(UsersInfo userInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        int UpdateUser(UsersInfo userInfo);



        /************************************************* 邮箱配置 EmailProfile *************************************************/
        /// <summary>
        /// 添加邮箱配置
        /// </summary>
        /// <param name="emailProfile">邮箱配置实体</param>
        int InsertEmailProfile(EmailProfileInfo emailProfile);
        /// <summary>
        /// 获取邮箱配置
        /// </summary>
        /// <param name="ID">邮箱配置编号</param>
        EmailProfileInfo GetEmailProfileByID(int ID);
        /// <summary>
        /// 获取邮箱配置
        /// </summary>
        /// <param name="emailProfile">邮箱配置实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<EmailProfileInfo> GetEmailProfile(EmailProfileInfo emailProfile, PageInfo pageInfo);
        /// <summary>
        /// 修改邮箱配置
        /// </summary>
        /// <param name="emailProfile">邮箱配置实体</param>
        int UpdateEmailProfile(EmailProfileInfo emailProfile);



        /************************************************* 邮件 Email *************************************************/
        /// <summary>
        /// 添加邮件
        /// </summary>
        /// <param name="emailInfo">邮件实体</param>
        int InsertEmail(EmailInfo emailInfo);
        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="ID">邮件编号</param>
        EmailInfo GetEmailByID(int ID);
        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="emailInfo">邮件实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<EmailInfo> GetEmail(EmailInfo emailInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改邮件
        /// </summary>
        /// <param name="emailInfo">邮件实体</param>
        int UpdateEmail(EmailInfo emailInfo);



        /************************************************* 邮箱地址 EmailAddress *************************************************/
        /// <summary>
        /// 添加邮箱地址
        /// </summary>
        /// <param name="emailAddressInfo">邮箱地址实体</param>
        int InsertEmailAddress(EmailAddressInfo emailAddressInfo);
        /// <summary>
        /// 获取邮箱地址
        /// </summary>
        /// <param name="ID">邮箱地址编号</param>
        EmailAddressInfo GetEmailAddressByID(int ID);
        /// <summary>
        /// 获取邮箱地址
        /// </summary>
        /// <param name="emailAddressInfo">邮箱地址实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<EmailAddressInfo> GetEmailAddress(EmailAddressInfo emailAddressInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改邮箱地址
        /// </summary>
        /// <param name="emailAddressInfo">邮箱地址实体</param>
        int UpdateEmailAddress(EmailAddressInfo emailAddressInfo);



        /************************************************* 邮件发送 EmailSend *************************************************/
        /// <summary>
        /// 添加邮件发送
        /// </summary>
        /// <param name="emailSendInfo">邮件发送实体</param>
        int InsertEmailSend(EmailSendInfo emailSendInfo);
        /// <summary>
        /// 获取邮件发送
        /// </summary>
        /// <param name="ID">邮件发送编号</param>
        EmailSendInfo GetEmailSendByID(int ID);
        /// <summary>
        /// 获取邮件发送
        /// </summary>
        /// <param name="emailSendInfo">邮件发送实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<EmailSendInfo> GetEmailSend(EmailSendInfo emailSendInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改邮件发送
        /// </summary>
        /// <param name="emailSendInfo">邮件发送实体</param>
        int UpdateEmailSend(EmailSendInfo emailSendInfo);



        /************************************************* 短信配置 MessageProfile *************************************************/
        /// <summary>
        /// 添加短信配置
        /// </summary>
        /// <param name="messageProfileInfo">短信配置实体</param>
        int InsertMessageProfile(MessageProfileInfo messageProfileInfo);
        /// <summary>
        /// 获取短信配置
        /// </summary>
        /// <param name="ID">短信配置编号</param>
        MessageProfileInfo GetMessageProfileByID(int ID);
        /// <summary>
        /// 获取短信配置
        /// </summary>
        /// <param name="messageProfileInfo">短信配置实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<MessageProfileInfo> GetMessageProfile(MessageProfileInfo messageProfileInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改短信配置
        /// </summary>
        /// <param name="messageProfileInfo">短信配置实体</param>
        int UpdateMessageProfile(MessageProfileInfo messageProfileInfo);



        /************************************************* 手机联系人 PhoneAddress *************************************************/
         /// <summary>
        /// 添加手机联系人
        /// </summary>
        /// <param name="phoneAddressInfo">手机联系人实体</param>
        int InsertPhoneAddress(PhoneAddressInfo phoneAddressInfo);
        /// <summary>
        /// 获取手机联系人
        /// </summary>
        /// <param name="ID">手机联系人编号</param>
        PhoneAddressInfo GetPhoneAddressByID(int ID);
        /// <summary>
        /// 获取手机联系人
        /// </summary>
        /// <param name="phoneAddressInfo">手机联系人实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<PhoneAddressInfo> GetPhoneAddress(PhoneAddressInfo phoneAddressInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改手机联系人
        /// </summary>
        /// <param name="phoneAddressInfo">手机联系人实体</param>
        int UpdatePhoneAddress(PhoneAddressInfo phoneAddressInfo);



        /************************************************* 短信发送 MessageSend *************************************************/
         /// <summary>
        /// 添加短信发送
        /// </summary>
        /// <param name="messageSendInfo">短信发送实体</param>
        int InsertMessageSend(MessageSendInfo messageSendInfo);
        /// <summary>
        /// 获取短信发送
        /// </summary>
        /// <param name="ID">短信发送编号</param>
        MessageSendInfo GetMessageSendByID(int ID);
        /// <summary>
        /// 获取短信发送
        /// </summary>
        /// <param name="messageSendInfo">短信发送实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<MessageSendInfo> GetMessageSend(MessageSendInfo messageSendInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改短信发送
        /// </summary>
        /// <param name="messageSendInfo">短信发送实体</param>
        int UpdateMessageSend(MessageSendInfo messageSendInfo);



        /************************************************* 帮助 Help *************************************************/
        /// <summary>
        /// 添加帮助
        /// </summary>
        /// <param name="helpInfo">帮助实体</param>
        int InsertHelp(HelpInfo helpInfo);
        /// <summary>
        /// 获取帮助
        /// </summary>
        /// <param name="ID">帮助编号</param>
        HelpInfo GetHelpByID(int ID);
        /// <summary>
        /// 获取帮助
        /// </summary>
        /// <param name="helpInfo">帮助实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        IList<HelpInfo> GetHelp(HelpInfo helpInfo, PageInfo pageInfo);
        /// <summary>
        /// 修改帮助
        /// </summary>
        /// <param name="helpInfo">帮助实体</param>
        int UpdateHelp(HelpInfo helpInfo);
    }
}
