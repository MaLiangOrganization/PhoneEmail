using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Start.Entity;

namespace Start.Web.Common
{
    public class Parameter
    {
        public static UsersInfo LoginUser()
        {
            string usersJson = MaLiang.Web.Utils.GetCookie(EnumConst.CookieAdmin);
            if (string.IsNullOrEmpty(usersJson)) return null;
            usersJson = MaLiang.Common.Security.Security.Decrypt(usersJson, EnumConst.PasswordKey);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UsersInfo>(usersJson);
        }
    }
}