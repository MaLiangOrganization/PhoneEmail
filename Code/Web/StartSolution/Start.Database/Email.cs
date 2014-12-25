using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加邮件
        /// </summary>
        /// <param name="emailInfo">邮件实体</param>
        public int InsertEmail(EmailInfo emailInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailInfo>().InsertOnSubmit(emailInfo);
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
        /// 获取邮件
        /// </summary>
        /// <param name="ID">邮件编号</param>
        public EmailInfo GetEmailByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<EmailInfo>().FirstOrDefault<EmailInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="emailInfo">邮件实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<EmailInfo> GetEmail(EmailInfo emailInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<EmailInfo> emailInfos =
                    (
                        from item in DB.GetTable<EmailInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (emailInfo != null && emailInfo.ID != 0)
                    emailInfos = emailInfos.Where<EmailInfo>(m => m.ID == emailInfo.ID);
                if (emailInfo != null && emailInfo.UserID != 0)
                    emailInfos = emailInfos.Where<EmailInfo>(m => m.UserID == emailInfo.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = emailInfos.Count();
                    return emailInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<EmailInfo>();
                }
                else
                    return emailInfos.ToList<EmailInfo>();
            }
        }

        /// <summary>
        /// 修改邮件
        /// </summary>
        /// <param name="emailInfo">邮件实体</param>
        public int UpdateEmail(EmailInfo emailInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailInfo>().Attach(emailInfo, true);
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
