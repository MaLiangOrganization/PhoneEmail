using System;
using System.Linq;
using Start.Entity;
using System.Collections.Generic;

namespace Start.Database
{
    public partial class Database : Start.IDatabase
    {
        /// <summary>
        /// 添加手机联系人
        /// </summary>
        /// <param name="phoneAddressInfo">手机联系人实体</param>
        public int InsertPhoneAddress(PhoneAddressInfo phoneAddressInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<PhoneAddressInfo>().InsertOnSubmit(phoneAddressInfo);
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
        /// 获取手机联系人
        /// </summary>
        /// <param name="ID">手机联系人编号</param>
        public PhoneAddressInfo GetPhoneAddressByID(int ID)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                return DB.GetTable<PhoneAddressInfo>().FirstOrDefault<PhoneAddressInfo>(u => u.ID == ID);
            }
        }

        /// <summary>
        /// 获取手机联系人
        /// </summary>
        /// <param name="phoneAddressInfo">手机联系人实体</param>
        /// <param name="pageInfo">分页实体</param>
        /// <returns></returns>
        public IList<PhoneAddressInfo> GetPhoneAddress(PhoneAddressInfo phoneAddressInfo, PageInfo pageInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                IQueryable<PhoneAddressInfo> phoneAddressInfos =
                    (
                        from item in DB.GetTable<PhoneAddressInfo>()
                        orderby item.ID descending
                        select item
                    );
                if (phoneAddressInfo != null && phoneAddressInfo.ID != 0)
                    phoneAddressInfos = phoneAddressInfos.Where<PhoneAddressInfo>(m => m.ID == phoneAddressInfo.ID);
                if (phoneAddressInfo != null && phoneAddressInfo.UserID != 0)
                    phoneAddressInfos = phoneAddressInfos.Where<PhoneAddressInfo>(m => m.UserID == phoneAddressInfo.UserID);

                if (pageInfo != null)
                {
                    pageInfo.TotalRecord = phoneAddressInfos.Count();
                    return phoneAddressInfos.Skip(pageInfo.PageSize * (pageInfo.PageIndex - 1)).Take(pageInfo.PageSize).ToList<PhoneAddressInfo>();
                }
                else
                    return phoneAddressInfos.ToList<PhoneAddressInfo>();
            }
        }

        /// <summary>
        /// 修改手机联系人
        /// </summary>
        /// <param name="phoneAddressInfo">手机联系人实体</param>
        public int UpdatePhoneAddress(PhoneAddressInfo phoneAddressInfo)
        {
            using (DataContextDB DB = new DataContextDB())
            {
                try
                {
                    DB.GetTable<PhoneAddressInfo>().Attach(phoneAddressInfo, true);
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
