using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSI.GymTech.Entity.Models;

namespace TSI.GymTech.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context() : base("GymTechConnection") { }
        
        public DbSet<AccessControl> AccessControl { get; set; }
        public DbSet<AccessLog> AccessLog { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AnamnesisSheet> AnamnesisSheet { get; set; }
        public DbSet<EvaluationSheet> EvaluationSheet { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentProduct> PaymentProduct { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<SheetAnswer> SheetAnswer { get; set; }
        public DbSet<SheetQuestion> SheetQuestion { get; set; }
        public DbSet<TrainingSheet> TrainingSheet { get; set; }
        public DbSet<TrainingSheetExercise> TrainingSheetExercise { get; set; }

        public static Context Create()
        {
            return new Context();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}