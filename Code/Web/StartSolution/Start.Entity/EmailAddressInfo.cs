using System;
using System.Data.Linq.Mapping;

namespace Start.Entity
{
    [Table(Name = "EmailAddress")]
    public class EmailAddressInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]
        public int ID { get; set; }

        [Column]
        public int Name { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string Phone { get; set; } 
    }
}
