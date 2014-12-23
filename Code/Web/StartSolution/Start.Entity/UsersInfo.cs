using System;
using System.Data.Linq.Mapping;

namespace Start.Entity
{
    [Table(Name="Users")]
    public class UsersInfo
    {
         [Column(IsPrimaryKey = true, IsDbGenerated = true, IsVersion = true)]	
        public int ID { get; set; }        
		      	
      	[Column]		
        public string Name { get; set; }        
		      	
      	[Column]		
        public string Email { get; set; }        
		      	
      	[Column]		
        public string Phone { get; set; }        
		      	
      	[Column]		
        public string Password { get; set; }        
		      	
      	[Column]		
        public string RealName { get; set; }        
		      	
      	[Column]		
        public DateTime Date { get; set; }        
		      	
      	[Column]		
        public int Status { get; set; }        
		      	
      	[Column]		
        public int UserGroup { get; set; }        
		      	
      	[Column]		
        public string Photo { get; set; }      
    }
}
