using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Users.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;
using Activites.Domain.Model;

namespace Shared.Persistence.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions options) : base(options){}

    public DbSet<Device> Devices { get; set; }
    public DbSet<Activity> Activities {get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Role>()
            .HasData(
                new Role {Id = 1, Name = "User", NormalizedName = "USER" },
                new Role {Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
            );

        //Devices
        builder.Entity<Device>().ToTable("Devices");
        builder.Entity<Device>().HasKey(p => p.Id);
        builder.Entity<Device>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Device>().Property(p => p.Info).IsRequired().HasMaxLength(600);
        builder.Entity<Device>().Property(p => p.UserType).IsRequired();
        builder.Entity<Device>().Property(p => p.UserId).IsRequired();

        //Relationships
        builder.Entity<User>().HasMany(p => p.Devices)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        //Activities
        builder.Entity<Activity>().ToTable("Activities");
        builder.Entity<Activity>().HasKey(p => p.Id);
        builder.Entity<Activity>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Activity>().Property(p => p.AppName).IsRequired().HasMaxLength(200);
        builder.Entity<Activity>().Property(p => p.TotalTimeUsedPerDay).IsRequired();
        builder.Entity<Activity>().Property(p => p.DateReported).IsRequired();
        builder.Entity<Activity>().Property(p => p.TotalNotifications).IsRequired();
        builder.Entity<Activity>().Property(p => p.DeviceId).IsRequired();

        //Relationships
        builder.Entity<Device>().HasMany(p => p.Activities)
            .WithOne(p => p.Device)
            .HasForeignKey(p => p.DeviceId);

        //Snake Case Conventions
        
        builder.UseSnakeCaseNamingConvention();   
    }

}