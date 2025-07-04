using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PERSISTENCE.Contexts;

public class AppDbContext : IdentityDbContext<AppUser, AppRole,int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserBlock> BlockedUsers{ get; set; }
    public DbSet<Banner> Banners{ get; set; }
    public DbSet<AuthRestriction> AuthRestrictions{ get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //App user

        builder.Entity<Friend>()
        .HasOne(f => f.SendedUser)
        .WithMany()
        .HasForeignKey(f => f.SenderUserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Friend>()
       .HasOne(f => f.RecieverUser)
       .WithMany()
       .HasForeignKey(f => f.RecieverUserId)
       .OnDelete(DeleteBehavior.Restrict);


        builder.Entity<AppUser>().Property(u => u.SecurityStatus).HasConversion<string>();
        builder.Entity<AppUser>().Property(u => u.Role).HasConversion<string>();
        builder.Entity<AppUser>()
        .HasMany(u => u.Posts)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SocialAccount>().Property(sa => sa.Id).UseIdentityColumn();
        builder.Entity<SocialAccount>()
        .HasOne(sa => sa.User)
        .WithMany(au => au.SocialAccounts)
        .HasForeignKey(sa => sa.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>().HasOne(c => c.AppUser)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>().HasOne(c => c.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PostLike>()
    .HasKey(pl => new { pl.PostId, pl.UserId }); // composite key

        builder.Entity<PostLike>()
            .HasOne(pl => pl.Post)
            .WithMany(p => p.PostLikes)
            .HasForeignKey(pl => pl.PostId);

        builder.Entity<PostLike>()
            .HasOne(pl => pl.AppUser)
            .WithMany(u => u.LikedPosts)
            .HasForeignKey(pl => pl.UserId);


        builder.Entity<Post>().Property(u => u.SecurityStatus).HasConversion<string>();
        base.OnModelCreating(builder);
    }
}
