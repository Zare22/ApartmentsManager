using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Public_MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class MyUser : IdentityUser<int, MyLogin, MyUserRole, MyClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class MyUserRole : IdentityUserRole<int> { }
    public class MyRole : IdentityRole<int, MyUserRole> { }
    public class MyClaim : IdentityUserClaim<int> { }
    public class MyLogin : IdentityUserLogin<int> { }

    public class ApplicationDbContext : IdentityDbContext<MyUser, MyRole, int, MyLogin, MyUserRole, MyClaim>
    {
        public ApplicationDbContext()
            : base("ApartmentsContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<MyUserRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<MyRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<MyClaim>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<MyLogin>().ToTable("AspNetUserLogins");

            modelBuilder.Entity<MyUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MyRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MyClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        public class MyPasswordHasher : PasswordHasher
        {
            public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
            {
                return providedPassword.Equals(providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
            }

            public override string HashPassword(string password)
            {
                return base.HashPassword(password);
            }
        }
    }
}