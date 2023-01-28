using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

public class MyUser : IdentityUser<int, MyLogin, MyUserRole, MyClaim>
{
    public string Address { get; set; }
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyUser, int> manager)
    {
        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
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
        modelBuilder.Entity<MyRole>().ToTable("AspNetRoles");
        modelBuilder.Entity<MyUserRole>().ToTable("AspNetUserRoles");
        modelBuilder.Entity<MyClaim>().ToTable("AspNetUserClaims");
        modelBuilder.Entity<MyLogin>().ToTable("AspNetUserLogins");



        modelBuilder.Entity<MyUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        modelBuilder.Entity<MyRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        modelBuilder.Entity<MyClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
    }
}