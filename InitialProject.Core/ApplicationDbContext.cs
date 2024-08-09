using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.Core.Entity.Others;
using Ecommerce.Core.Entity.Vendor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        //----------------------------------------------------------------------------------
        public virtual DbSet<Paths> Paths { get; set; }
        public virtual DbSet<Images> Images { get; set; }

        //----------------------------------------------------------------------------------
        public virtual DbSet<City> Cities { get; set; }

        //----------------------------------------------------------------------------------
        public virtual DbSet<Category> Categories { get; set; }
        //----------------------------------------------------------------------------------
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=LAPTOP-K8QC50ME;Database=Ecommerce;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "dbo");
        }
    }
}