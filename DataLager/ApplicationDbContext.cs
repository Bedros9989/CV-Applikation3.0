using Core.Models;
using DataLager.Areas.Identity.Data;
using DataLager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLager;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<CV> CV { get; set; }
    public DbSet<Erfarenhet> Erfarenhet { get; set; }
    public DbSet<Kompetenser> Kompetenser { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ProjektDeltagare> ProjektDeltagare { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProjektDeltagare>()
            .HasKey(pd => new { pd.UserId, pd.ProjectId });

        modelBuilder.Entity<ProjektDeltagare>()
            .HasOne(pd => pd.User)
            .WithMany(u => u.Deltagare)
            .HasForeignKey(pd => pd.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjektDeltagare>()
            .HasOne(pd => pd.Project)
            .WithMany(p => p.Deltagare)
            .HasForeignKey(pd => pd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }

}
