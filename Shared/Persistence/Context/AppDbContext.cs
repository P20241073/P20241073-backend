using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Users.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;

namespace Shared.Persistence.Context;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public AppDbContext(DbContextOptions options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Role>()
            .HasData(
                new Role {Id = 1, Name = "User", NormalizedName = "USER" },
                new Role {Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
            );

        //Snake Case Conventions
        
        builder.UseSnakeCaseNamingConvention();   
    }

}