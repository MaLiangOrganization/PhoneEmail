using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        public int InsertUser(UsersInfo userInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<UsersInfo>().InsertOnSubmit(userInfo);
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
        /// 获取用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UsersInfo GetUserByNameAndPassword(string name, string password)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<UsersInfo>().FirstOrDefault<UsersInfo>(u => u.Name == name && u.Password == password);
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="ID">用户编号</param>
        public UsersInfo GetUserByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<UsersInfo>().FirstOrDefault<UsersInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<UsersInfo> GetUser(UsersInfo userInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<UsersInfo> userInfos =
                    (
                        from item in DB.GetTable<UsersInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (userInfo != null && userInfo.ID != 0)
                    userInfos = userInfos.Where<UsersInfo>(m => m.ID == userInfo.ID);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Name))
                    userInfos = userInfos.Where<UsersInfo>(m => m.Name.Contains(userInfo.Name));
                if (userInfo != null && userInfo.UserGroup != 0)
                    userInfos = userInfos.Where<UsersInfo>(m => m.UserGroup == userInfo.UserGroup);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = userInfos.Count();
                    return userInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<UsersInfo>();
                }
                else
                    return userInfos.ToList<UsersInfo>();
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userInfo">用户实体</param>
        public int UpdateUser(UsersInfo userInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<UsersInfo>().Attach(userInfo, true);
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
