namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TSI.GymTech.Entity.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TSI.GymTech.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(TSI.GymTech.Data.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Insert administrator user on the database
            context.Person.AddOrUpdate(p => p.Name,
              new Person
              {
                  Name = "Administrator",
                  Email = "admin@tsi.com.br",
                  Password = "admin",
                  SocialSecurityCard = "admin",
                  ProfileType = Entity.Enumerates.PersonType.Administrator
              }
            );
        }
    }
}
