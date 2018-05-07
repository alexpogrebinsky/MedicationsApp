namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Models.MedicineDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public void LoadRows()
        {
            using (MedicineDB db = new MedicineDB())
            {
                UserTransaction trans1 = new UserTransaction
                {
                    Quantity = 10,
                    CreatedByDate = new DateTime(2018, 04, 20),
                    
                    //  CreatedByUser = new AppUser { Name = "Person1" },
                    //  ModifiedByUser = new AppUser { Name = "Person2" },
                    Med = new Medication { Description = "Description One", AlertThreshold = 5 }

                };

                UserTransaction trans2 = new UserTransaction
                {
                    Quantity = 10,
                    CreatedByDate = new DateTime(2018, 06, 10),
                    
                    //  CreatedByUser = new AppUser { Name = "Person3" },
                    //  ModifiedByUser = new AppUser { Name = "Person4" },
                    Med = new Medication { Description = "Description Two", AlertThreshold = 6 }

                };

                db.UserTransactions.Add(trans1);
                db.UserTransactions.Add(trans2);

                db.SaveChanges();
            }

        }

        protected override void Seed(Data.Models.MedicineDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
          // LoadRows();

        }
    }
}


