using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加邮箱配置
        /// </summary>
        /// <param name="emailProfile">邮箱配置实体</param>
        public int InsertEmailProfile(EmailProfileInfo emailProfile)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailProfileInfo>().InsertOnSubmit(emailProfile);
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
        /// 获取邮箱配置
        /// </summary>
        /// <param name="ID">邮箱配置编号</param>
        public EmailProfileInfo GetEmailProfileByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<EmailProfileInfo>().FirstOrDefault<EmailProfileInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取邮箱配置
        /// </summary>
        /// <param name="emailProfile">邮箱配置实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<EmailProfileInfo> GetEmailProfile(EmailProfileInfo emailProfile, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<EmailProfileInfo> emailProfiles =
                    (
                        from item in DB.GetTable<EmailProfileInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (emailProfile != null && emailProfile.ID != 0)
                    emailProfiles = emailProfiles.Where<EmailProfileInfo>(m => m.ID == emailProfile.ID);
                if (emailProfile != null && emailProfile.UserID != 0)
                    emailProfiles = emailProfiles.Where<EmailProfileInfo>(m => m.UserID == emailProfile.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = emailProfiles.Count();
                    return emailProfiles.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<EmailProfileInfo>();
                }
                else
                    return emailProfiles.ToList<EmailProfileInfo>();
            }
        }

        /// <summary>
        /// 修改邮箱配置
        /// </summary>
        /// <param name="emailProfile">邮箱配置实体</param>
        public int UpdateEmailProfile(EmailProfileInfo emailProfile)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<EmailProfileInfo>().Attach(emailProfile, true);
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
