using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加帮助
        /// </summary>
        /// <param name="helpInfo">帮助实体</param>
        public int InsertHelp(HelpInfo helpInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<HelpInfo>().InsertOnSubmit(helpInfo);
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
        /// 获取帮助
        /// </summary>
        /// <param name="ID">帮助编号</param>
        public HelpInfo GetHelpByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<HelpInfo>().FirstOrDefault<HelpInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取帮助
        /// </summary>
        /// <param name="helpInfo">帮助实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<HelpInfo> GetHelp(HelpInfo helpInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<HelpInfo> helpInfos =
                    (
                        from item in DB.GetTable<HelpInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (helpInfo != null && helpInfo.ID != 0)
                    helpInfos = helpInfos.Where<HelpInfo>(m => m.ID == helpInfo.ID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = helpInfos.Count();
                    return helpInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<HelpInfo>();
                }
                else
                    return helpInfos.ToList<HelpInfo>();
            }
        }

        /// <summary>
        /// 修改帮助
        /// </summary>
        /// <param name="helpInfo">帮助实体</param>
        public int UpdateHelp(HelpInfo helpInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<HelpInfo>().Attach(helpInfo, true);
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
