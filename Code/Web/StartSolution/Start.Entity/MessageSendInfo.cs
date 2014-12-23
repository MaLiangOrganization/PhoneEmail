using System;
using System.Data.Linq.Mapping;

namespace Start.Entity
{
    [Table(Name = "MessageSend")]
    public class MessageSendInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]
        public int ID { get; set; }

        [Column]
        public int UserID { get; set; }

        [Column]
        public string Memo { get; set; }

        [Column]
        public string ReceivePhone { get; set; }      
    }
}
