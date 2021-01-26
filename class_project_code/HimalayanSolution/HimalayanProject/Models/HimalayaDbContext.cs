using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HimalayanProject.Models
{
    public partial class HimalayaDbContext : DbContext
    {
        public HimalayaDbContext()
        {
        }

        public HimalayaDbContext(DbContextOptions<HimalayaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Expedition> Expeditions { get; set; }
        public virtual DbSet<Peak> Peaks { get; set; }
        public virtual DbSet<TrekkingAgency> TrekkingAgencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=HimalayaDbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expedition>(entity =>
            {
                entity.ToTable("Expedition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PeakId).HasColumnName("PeakID");

                entity.Property(e => e.Season).HasMaxLength(10);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TerminationReason).HasMaxLength(80);

                entity.Property(e => e.TrekkingAgencyId).HasColumnName("TrekkingAgencyID");

                entity.HasOne(d => d.Peak)
                    .WithMany(p => p.Expeditions)
                    .HasForeignKey(d => d.PeakId)
                    .HasConstraintName("Expedition_FK_Peak");

                entity.HasOne(d => d.TrekkingAgency)
                    .WithMany(p => p.Expeditions)
                    .HasForeignKey(d => d.TrekkingAgencyId)
                    .HasConstraintName("Expedition_FK_TrekkingAgency");
            });

            modelBuilder.Entity<Peak>(entity =>
            {
                entity.ToTable("Peak");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TrekkingAgency>(entity =>
            {
                entity.ToTable("TrekkingAgency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
