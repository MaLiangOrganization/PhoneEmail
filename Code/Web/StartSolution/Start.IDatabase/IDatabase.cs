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
        /// 获取用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UsersInfo GetUserByNameAndPassword(string name, string password);
    }
}
