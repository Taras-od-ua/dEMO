using System;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Context
{
    public partial class ListsContext : DbContext
    {
        public ListsContext(DbContextOptions<ListsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<List>(entity =>
            {
                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.is_deleted).IsUnicode(false);
                entity.Property(e => e.created_at).IsUnicode(false);
                entity.Property(e => e.updated_at).IsUnicode(false);

                 entity.HasOne(d => d.Category)
                     .WithMany(p => p.Lists)
                     .HasForeignKey(d => d.CategoryId)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FK_lists_cats");
            });

            /*modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });*/
        }
    }
}
