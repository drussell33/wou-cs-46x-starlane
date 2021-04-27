using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using iCollections.Models;

#nullable disable

namespace iCollections.Data
{
    public partial class ICollectionsDbContext : DbContext
    {
        public ICollectionsDbContext()
        {
        }

        public ICollectionsDbContext(DbContextOptions<ICollectionsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionKeyword> CollectionKeywords { get; set; }
        public virtual DbSet<CollectionPhoto> CollectionPhotos { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<FriendsWith> FriendsWiths { get; set; }
        public virtual DbSet<IcollectionUser> IcollectionUsers { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ICollectionsConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("Collection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateMade)
                    .HasColumnType("datetime")
                    .HasColumnName("date_made");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Route)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("route");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Visibility).HasColumnName("visibility");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Collection_fk_ICollectionUser");
            });

            modelBuilder.Entity<CollectionKeyword>(entity =>
            {
                entity.ToTable("CollectionKeyword");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CollectId).HasColumnName("collect_id");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasColumnName("date_added");

                entity.Property(e => e.KeywordId).HasColumnName("keyword_id");

                entity.HasOne(d => d.Collect)
                    .WithMany(p => p.CollectionKeywords)
                    .HasForeignKey(d => d.CollectId)
                    .HasConstraintName("CollectionKeyword_fk_Collection");

                entity.HasOne(d => d.Keyword)
                    .WithMany(p => p.CollectionKeywords)
                    .HasForeignKey(d => d.KeywordId)
                    .HasConstraintName("CollectionKeyword_fk_Keyword");
            });

            modelBuilder.Entity<CollectionPhoto>(entity =>
            {
                entity.ToTable("CollectionPhoto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CollectId).HasColumnName("collect_id");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasColumnName("date_added");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.PhotoId).HasColumnName("photo_id");

                entity.Property(e => e.PhotoRank).HasColumnName("photo_rank");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne(d => d.Collect)
                    .WithMany(p => p.CollectionPhotoes)
                    .HasForeignKey(d => d.CollectId)
                    .HasConstraintName("CollectionPhoto_fk_Collection");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.CollectionPhotoes)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("CollectionPhoto_fk_Photo");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("Follow");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Began)
                    .HasColumnType("datetime")
                    .HasColumnName("began");

                entity.Property(e => e.Followed).HasColumnName("followed");

                entity.Property(e => e.Follower).HasColumnName("follower");

                entity.HasOne(d => d.FollowedNavigation)
                    .WithMany(p => p.FollowFollowedNavigations)
                    .HasForeignKey(d => d.Followed)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Follow_fk_ICollectionUser_Two");

                entity.HasOne(d => d.FollowerNavigation)
                    .WithMany(p => p.FollowFollowerNavigations)
                    .HasForeignKey(d => d.Follower)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Follow_fk_ICollectionUser_One");
            });

            modelBuilder.Entity<FriendsWith>(entity =>
            {
                entity.ToTable("FriendsWith");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Began)
                    .HasColumnType("datetime")
                    .HasColumnName("began");

                entity.Property(e => e.User1Id).HasColumnName("user1_id");

                entity.Property(e => e.User2Id).HasColumnName("user2_id");

                entity.HasOne(d => d.User1)
                    .WithMany(p => p.FriendsWithUser1)
                    .HasForeignKey(d => d.User1Id)
                    .HasConstraintName("FriendsWith_fk_ICollectionUser_One");

                entity.HasOne(d => d.User2)
                    .WithMany(p => p.FriendsWithUser2)
                    .HasForeignKey(d => d.User2Id)
                    .HasConstraintName("FriendsWith_fk_ICollectionUser_Two");
            });

            modelBuilder.Entity<IcollectionUser>(entity =>
            {
                entity.ToTable("ICollectionUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AboutMe)
                    .HasMaxLength(250)
                    .HasColumnName("about_me");

                entity.Property(e => e.AspnetIdentityId)
                    .HasMaxLength(450)
                    .HasColumnName("ASPNetIdentityID");

                entity.Property(e => e.DateJoined)
                    .HasColumnType("datetime")
                    .HasColumnName("date_joined");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.ProfilePicId).HasColumnName("profile_pic_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.ToTable("Keyword");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.DateUploaded)
                    .HasColumnType("datetime")
                    .HasColumnName("date_uploaded");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PhotoGuid)
                    .HasColumnName("PhotoGUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Photo_fk_ICollectionUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
