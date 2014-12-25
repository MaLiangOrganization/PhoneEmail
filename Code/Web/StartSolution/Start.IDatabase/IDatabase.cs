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
    }
}
