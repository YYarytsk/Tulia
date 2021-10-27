using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.Models.Entities
{
    public partial class tuliadbContext : DbContext
    {
        public tuliadbContext()
        {
        }

        public tuliadbContext(DbContextOptions<tuliadbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__PostId__68487DD7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__UserId__6754599E");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.GroupTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Groups__UserId__6B24EA82");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("Membership");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Membershi__Group__72C60C4A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Membershi__UserI__71D1E811");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Body)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Posts__GroupId__7B5B524B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Posts__UserId__60A75C0F");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportContent)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reports__PostId__7A672E12");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reports__UserId__797309D9");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
