using System;
using System.Data.Linq.Mapping;

namespace Start.Entity
{
    [Table(Name = "EmailProfile")]
    public class EmailProfileInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]
        public int ID { get; set; }

        [Column]
        public int UserID { get; set; }

        [Column]
        public string SMTP { get; set; }

        [Column]
        public string SMTPPort { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public int SSL { get; set; }      
    }
}
