using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _2_UsuarioAPI.Data
{
    public class UserDBContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDBContext(DbContextOptions<UserDBContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            int stringMaxLength = 200;

            builder.Entity<IdentityRole>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityRole>(x => x.Property(m => m.NormalizedName).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUser>(x => x.Property(m => m.NormalizedUserName).HasMaxLength(stringMaxLength));

            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(stringMaxLength));

            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));

            IdentityUser<int> admin = new IdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 99999,
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

            admin.PasswordHash = hasher.HashPassword(admin,
                _configuration.GetValue<string>("admininfo:password"));

            builder.Entity<IdentityUser<int>>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 99997, Name = "regular", NormalizedName = "REGULAR" }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 }
                );
        }
    }
}