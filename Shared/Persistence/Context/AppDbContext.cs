using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Users.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;

namespace Shared.Persistence.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions options) : base(options){}

    public DbSet<Device> Devices { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Role>()
            .HasData(
                new Role {Id = 1, Name = "User", NormalizedName = "USER" },
                new Role {Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
            );

        //Customers
        builder.Entity<Device>().ToTable("Devices");
        builder.Entity<Device>().HasKey(p => p.Id);
        builder.Entity<Device>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Device>().Property(p => p.Info).IsRequired().HasMaxLength(600);
        builder.Entity<Device>().Property(p => p.UserType).IsRequired();
        builder.Entity<Device>().Property(p => p.UserId).IsRequired();

        //Snake Case Conventions
        
        builder.UseSnakeCaseNamingConvention();   
    }

}