using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Start
{
    public class DatabaseProvider
    {
        private static IDatabase instance = null;
        private static object locks = new object();

        /// <summary>
        /// 私有函数，禁止外部访问
        /// </summary>
        private DatabaseProvider() { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>IDatabase接口IDatabase</returns>
        public static IDatabase Instance()
        {
            if (instance == null)
            {
                lock (locks)
                {
                    if (instance == null)
                    {
                        instance = (IDatabase)System.Activator.CreateInstance(Type.GetType("Start.Database.Database, Start.Database", true, false));
                    }
                }
            }
            return instance;
        }
    }
}
