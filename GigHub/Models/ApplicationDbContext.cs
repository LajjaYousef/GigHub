using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // I do This to Allow Database To Know my Models that i do this is called [Refrence] to Allow EF Know it
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);
            // here i do relation between follwer and followee  [the user and gig]
            modelBuilder.Entity<ApplicationUser>()
                // i use HasMany beacuase the user can follow many gig
                .HasMany(u => u.followers)
                .WithRequired(f => f.followee)
                .WillCascadeOnDelete(false);

            // here i do relation between follwee and follower  [the gig and user]

            modelBuilder.Entity<ApplicationUser>()
                // i use HasMany beacuase the gig can have many user follow him
                .HasMany(u => u.followee)
                .WithRequired(f => f.follower)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

    }
}