using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Models
{


    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        
        public string Name { get; set; } 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class MedicineDB : IdentityDbContext<AppUser>
    {
        public MedicineDB()
            : base("Context", throwIfV1Schema: false)
        {
        }

        public static MedicineDB Create()
        {
            return new MedicineDB();
        }

         
        public DbSet<UserTransaction> UserTransactions { get; set; }
        public DbSet<Medication> Medications { get; set; }
    }
}