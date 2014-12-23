using System;
using System.Data.Linq.Mapping;


namespace Start.Entity
{
    [Table(Name = "Help")]
    public class HelpInfo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Memo { get; set; }

        [Column]
        public int Type { get; set; }

        [Column]
        public DateTime Date { get; set; }

        [Column]
        public int Status { get; set; }      
    }
}
