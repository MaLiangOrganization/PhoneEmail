using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加邮箱地址
        /// </summary>
        /// <param name="emailAddressInfo">邮箱地址实体</param>
        public int InsertEmailAddress(EmailAddressInfo emailAddressInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailAddressInfo>().InsertOnSubmit(emailAddressInfo);
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
        /// 获取邮箱地址
        /// </summary>
        /// <param name="ID">邮箱地址编号</param>
        public EmailAddressInfo GetEmailAddressByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<EmailAddressInfo>().FirstOrDefault<EmailAddressInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取邮箱地址
        /// </summary>
        /// <param name="emailAddressInfo">邮箱地址实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<EmailAddressInfo> GetEmailAddress(EmailAddressInfo emailAddressInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<EmailAddressInfo> emailAddressInfos =
                    (
                        from item in DB.GetTable<EmailAddressInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (emailAddressInfo != null && emailAddressInfo.ID != 0)
                    emailAddressInfos = emailAddressInfos.Where<EmailAddressInfo>(m => m.ID == emailAddressInfo.ID);
                if (emailAddressInfo != null && emailAddressInfo.UserID != 0)
                    emailAddressInfos = emailAddressInfos.Where<EmailAddressInfo>(m => m.UserID == emailAddressInfo.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = emailAddressInfos.Count();
                    return emailAddressInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<EmailAddressInfo>();
                }
                else
                    return emailAddressInfos.ToList<EmailAddressInfo>();
            }
        }

        /// <summary>
        /// 修改邮箱地址
        /// </summary>
        /// <param name="emailAddressInfo">邮箱地址实体</param>
        public int UpdateEmailAddress(EmailAddressInfo emailAddressInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailAddressInfo>().Attach(emailAddressInfo, true);
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
