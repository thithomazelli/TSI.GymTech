using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TSI.GymTech.WebAPI.Models
{
    public class TSIGymTechWebAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TSIGymTechWebAPIContext() : base("name=TSIGymTechWebAPIContext")
        {
        }

        public System.Data.Entity.DbSet<TSI.GymTech.Entity.Models.Person> People { get; set; }
    }
}
