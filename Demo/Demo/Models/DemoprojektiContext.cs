using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Demo.Models
{
    public partial class DemoprojektiContext : DbContext
    {
        public DemoprojektiContext()
        {
        }

        public DemoprojektiContext(DbContextOptions<DemoprojektiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aine> Aines { get; set; }
        public virtual DbSet<Keskustelu> Keskustelus { get; set; }
        public virtual DbSet<Kommentti> Kommenttis { get; set; }
        public virtual DbSet<Raakaaine> Raakaaines { get; set; }
        public virtual DbSet<Resepti> Reseptis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Demoprojekti;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Aine>(entity =>
            {
                entity.ToTable("Aine");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nimi");
            });

            modelBuilder.Entity<Keskustelu>(entity =>
            {
                entity.ToTable("Keskustelu");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Luotu)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("luotu");

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nimi");
            });

            modelBuilder.Entity<Kommentti>(entity =>
            {
                entity.ToTable("Kommentti");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Keskustelu).HasColumnName("keskustelu");

                entity.Property(e => e.Luotu)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("luotu");

                entity.Property(e => e.Otsikko)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("otsikko");

                entity.Property(e => e.Tekija)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tekija");

                entity.Property(e => e.Teksti)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("teksti");

                entity.HasOne(d => d.KeskusteluNavigation)
                    .WithMany(p => p.Kommenttis)
                    .HasForeignKey(d => d.Keskustelu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kommentti_Keskustelu");
            });

            modelBuilder.Entity<Raakaaine>(entity =>
            {
                entity.ToTable("Raakaaine");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ainesosa).HasColumnName("ainesosa");

                entity.Property(e => e.Maara).HasColumnName("maara");

                entity.Property(e => e.Resepti).HasColumnName("resepti");

                entity.Property(e => e.Yksikko)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("yksikko")
                    .IsFixedLength(true);

                entity.HasOne(d => d.AinesosaNavigation)
                    .WithMany(p => p.Raakaaines)
                    .HasForeignKey(d => d.Ainesosa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raakaaine_Aine");

                entity.HasOne(d => d.ReseptiNavigation)
                    .WithMany(p => p.Raakaaines)
                    .HasForeignKey(d => d.Resepti)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raakaaine_Resepti");
            });

            modelBuilder.Entity<Resepti>(entity =>
            {
                entity.ToTable("Resepti");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Aika).HasColumnName("aika");

                entity.Property(e => e.Luotu)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("luotu");

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nimi");

                entity.Property(e => e.Ohje)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("ohje");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
