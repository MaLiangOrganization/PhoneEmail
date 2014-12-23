using System;
using System.Data.Linq.Mapping;

namespace Start.Entity
{
    [Table(Name = "EmailProfile")]
    public class EmailSendInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]
        public int ID { get; set; }

        [Column]
        public int UserID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Memo { get; set; }

        [Column]
        public string ReceiveEmail { get; set; }

        [Column]
        public DateTime Date { get; set; }     
    }
}
