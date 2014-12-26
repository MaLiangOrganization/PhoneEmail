using System;

namespace Start.Entity
{
    public enum EnumStatus
    {
        NO = -1,
        YES = 1
    }

    public enum EnumUserGroup
    {
        User = 1,
        Admin = 10
    }

    public enum EnumHelp
    {
        Email = 10,
        Phone = 20
    }

    public struct EnumConst
    {
        public static readonly string PasswordKey = "1@23#!A!";

        public static readonly string CookieAdmin = "PhoneEmailCookieAdmin";
    }
}
