using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFriendsV3._5.Models;

namespace MyFriendsV3._5.Data
{
    public class MyFriendsV3_5Context : DbContext
    {
        public MyFriendsV3_5Context (DbContextOptions<MyFriendsV3_5Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between User and Picture
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserPictures)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            // Configure the one-to-one relationship between User and ProfilePicture
            modelBuilder.Entity<User>()
                .HasOne(u => u.ProfilePicture)
                .WithOne()
                .HasForeignKey<User>(u => u.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }

        public DbSet<Picture> Picture { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
    }
}
