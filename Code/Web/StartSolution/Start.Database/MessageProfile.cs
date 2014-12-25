using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加短信配置
        /// </summary>
        /// <param name="messageProfileInfo">短信配置实体</param>
        public int InsertMessageProfile(MessageProfileInfo messageProfileInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<MessageProfileInfo>().InsertOnSubmit(messageProfileInfo);
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
        /// 获取短信配置
        /// </summary>
        /// <param name="ID">短信配置编号</param>
        public MessageProfileInfo GetMessageProfileByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<MessageProfileInfo>().FirstOrDefault<MessageProfileInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取短信配置
        /// </summary>
        /// <param name="messageProfileInfo">短信配置实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<MessageProfileInfo> GetMessageProfile(MessageProfileInfo messageProfileInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<MessageProfileInfo> messageProfileInfos =
                    (
                        from item in DB.GetTable<MessageProfileInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (messageProfileInfo != null && messageProfileInfo.ID != 0)
                    messageProfileInfos = messageProfileInfos.Where<MessageProfileInfo>(m => m.ID == messageProfileInfo.ID);
                if (messageProfileInfo != null && messageProfileInfo.UserID != 0)
                    messageProfileInfos = messageProfileInfos.Where<MessageProfileInfo>(m => m.UserID == messageProfileInfo.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = messageProfileInfos.Count();
                    return messageProfileInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<MessageProfileInfo>();
                }
                else
                    return messageProfileInfos.ToList<MessageProfileInfo>();
            }
        }

        /// <summary>
        /// 修改短信配置
        /// </summary>
        /// <param name="messageProfileInfo">短信配置实体</param>
        public int UpdateMessageProfile(MessageProfileInfo messageProfileInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<MessageProfileInfo>().Attach(messageProfileInfo, true);
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
