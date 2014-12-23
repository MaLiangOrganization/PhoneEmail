using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
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
    }
}
