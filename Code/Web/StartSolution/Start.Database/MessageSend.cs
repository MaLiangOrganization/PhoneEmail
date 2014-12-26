using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加短信发送
        /// </summary>
        /// <param name="messageSendInfo">短信发送实体</param>
        public int InsertMessageSend(MessageSendInfo messageSendInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<MessageSendInfo>().InsertOnSubmit(messageSendInfo);
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
        /// 获取短信发送
        /// </summary>
        /// <param name="ID">短信发送编号</param>
        public MessageSendInfo GetMessageSendByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<MessageSendInfo>().FirstOrDefault<MessageSendInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取短信发送
        /// </summary>
        /// <param name="messageSendInfo">短信发送实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<MessageSendInfo> GetMessageSend(MessageSendInfo messageSendInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<MessageSendInfo> messageSendInfos =
                    (
                        from item in DB.GetTable<MessageSendInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (messageSendInfo != null && messageSendInfo.ID != 0)
                    messageSendInfos = messageSendInfos.Where<MessageSendInfo>(m => m.ID == messageSendInfo.ID);
                if (messageSendInfo != null && messageSendInfo.UserID != 0)
                    messageSendInfos = messageSendInfos.Where<MessageSendInfo>(m => m.UserID == messageSendInfo.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = messageSendInfos.Count();
                    return messageSendInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<MessageSendInfo>();
                }
                else
                    return messageSendInfos.ToList<MessageSendInfo>();
            }
        }

        /// <summary>
        /// 修改短信发送
        /// </summary>
        /// <param name="messageSendInfo">短信发送实体</param>
        public int UpdateMessageSend(MessageSendInfo messageSendInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<MessageSendInfo>().Attach(messageSendInfo, true);
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
