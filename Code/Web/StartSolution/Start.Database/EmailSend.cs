using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加邮件发送
        /// </summary>
        /// <param name="emailSendInfo">邮件发送实体</param>
        public int InsertEmailSend(EmailSendInfo emailSendInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailSendInfo>().InsertOnSubmit(emailSendInfo);
                    DB.SubmitChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    return -1;
                }
            }
        }

        /// <summary>
        /// 获取邮件发送
        /// </summary>
        /// <param name="ID">邮件发送编号</param>
        public EmailSendInfo GetEmailSendByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<EmailSendInfo>().FirstOrDefault<EmailSendInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取邮件发送
        /// </summary>
        /// <param name="emailSendInfo">邮件发送实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<EmailSendInfo> GetEmailSend(EmailSendInfo emailSendInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<EmailSendInfo> emailSendInfos =
                    (
                        from item in DB.GetTable<EmailSendInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (emailSendInfo != null && emailSendInfo.ID != 0)
                    emailSendInfos = emailSendInfos.Where<EmailSendInfo>(m => m.ID == emailSendInfo.ID);
                if (emailSendInfo != null && emailSendInfo.UserID != 0)
                    emailSendInfos = emailSendInfos.Where<EmailSendInfo>(m => m.UserID == emailSendInfo.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = emailSendInfos.Count();
                    return emailSendInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<EmailSendInfo>();
                }
                else
                    return emailSendInfos.ToList<EmailSendInfo>();
            }
        }

        /// <summary>
        /// 修改邮件发送
        /// </summary>
        /// <param name="emailSendInfo">邮件发送实体</param>
        public int UpdateEmailSend(EmailSendInfo emailSendInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailSendInfo>().Attach(emailSendInfo, true);
                    DB.SubmitChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    return -1;
                }
            }
        }
    }
}
