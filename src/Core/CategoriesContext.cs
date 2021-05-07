using System;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Context
{
    public partial class CategoriesContext : DbContext
    {
        public CategoriesContext(DbContextOptions<CategoriesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        //public virtual DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.Color).IsUnicode(false);
                entity.Property(e => e.is_deleted).IsUnicode(false);
                entity.Property(e => e.created_at).IsUnicode(false);
                entity.Property(e => e.updated_at).IsUnicode(false);

                /* entity.HasOne(d => d.Owner)
                     .WithMany(p => p.Categories)
                     .HasForeignKey(d => d.OwnerId)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_Cats_Owners");*/
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });
        }
    }
}
