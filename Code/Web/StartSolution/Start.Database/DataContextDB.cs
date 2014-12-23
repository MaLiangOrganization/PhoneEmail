using System;
using System.Data.Linq;


namespace Start.Database
{
    public class DataContextDB : DataContext
    {
        /// <summary>
        /// 默认构造函数。
        /// </summary>
        public DataContextDB()
            : base(System.Configuration.ConfigurationManager.AppSettings["SqlConnection"])
        {
        }
    }
}
