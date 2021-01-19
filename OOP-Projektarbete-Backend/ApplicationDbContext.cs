using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOP_Projektarbete_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsersMovies> UsersMovies { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UsersMovies>().HasKey(x => new { x.Id });

            builder.Entity<Message>()
                .HasOne(x => x.User)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Friend>()
                .HasOne(x => x.RequestedBy)
                .WithMany(x => x.SentFriendRequests)
                .HasForeignKey(x => x.RequestedById);

            builder.Entity<Friend>()
                .HasOne(x => x.RequestedTo)
                .WithMany(x => x.ReceievedFriendRequests)
                .HasForeignKey(x => x.RequestedToId);
        }
    }
}