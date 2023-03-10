using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); //for identity tables

        //https://guidgenerator.com/online-guid-generator.aspx
            var adminsRoleId = "e62a9c73-474e-4ed4-9c2c-3b71e3431461";
            var usersRoleId = "f87e3da7-3d52-4ea3-900d-f4723aaa0bde";

            builder.Entity<IdentityRole>().HasData(
             new IdentityRole { Id = adminsRoleId, Name = "Admins", NormalizedName = "ADMINS"},
             new IdentityRole { Id = usersRoleId, Name = "Users", NormalizedName = "USERS" }
             );

            var hasher = new PasswordHasher<ApplicationUser>();

            var adminId = "99551026-e7fd-42f2-9f05-eec450b4fe81";

            builder.Entity<ApplicationUser>().HasData(
             new ApplicationUser
             {
                 Id = adminId,
                 UserName = "admin",
                 NormalizedUserName = "ADMIN",
                 Email = "admin@x.x",
                 NormalizedEmail = "ADMIN@X.X",
                 PasswordHash = hasher.HashPassword(null, "Secret_123"),
                 PhoneNumberConfirmed = true,
                 EmailConfirmed = true,
             }
             );

            builder.Entity<IdentityUserRole<string>>().HasData(
             new IdentityUserRole<string> { RoleId = adminsRoleId, UserId = adminId }
             );

            //builder.Entity<Product>().HasData(
            // new Product { Id = 1, Name = "Kayak", Category = "Watersports", Price = 275 },
            // new Product { Id = 2, Name = "Lifejacket", Category = "Watersports", Price = 48.95m },
            // new Product { Id = 3, Name = "Soccer Ball", Category = "Soccer", Price = 19.50m },
            // new Product { Id = 4, Name = "Corner Flags", Category = "Soccer", Price = 34.95m },
            // new Product { Id = 5, Name = "Stadium", Category = "Soccer", Price = 79500 },
            // new Product { Id = 6, Name = "Thinking Cap", Category = "Chess", Price = 16 },
            // new Product { Id = 7, Name = "Unsteady Chair", Category = "Chess", Price = 29.95m },
            // new Product { Id = 8, Name = "Human Chess Board", Category = "Chess", Price = 75 },
            // new Product { Id = 9, Name = "Bling-Bling King", Category = "Chess", Price = 1200 });
        }
    }
}
