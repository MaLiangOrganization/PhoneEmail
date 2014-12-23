using System;
using System.Data.Linq.Mapping;

namespace Start.Entity
{
    [Table(Name = "Email")]
    public class EmailInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]
        public int ID { get; set; }

        [Column]
        public int UserID { get; set; }

        [Column]
        public string Subject { get; set; }

        [Column]
        public string Memo { get; set; }     
    }
}
