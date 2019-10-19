using Hbsis.Library.CrossCutting;
using Hbsis.Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace Hbsis.Library.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder = GetConfiguration(optionsBuilder);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public virtual void UpdateDatabase()
        {
            if (Database.IsInMemory())
            {
                Database.EnsureCreated();

                return;
            }

            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.RemoveCascadeDeleteBehavior();
            modelBuilder.SetDateTimeColumnType();
        }

        protected virtual DbContextOptionsBuilder GetConfiguration(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(EnvironmentProperties.ConnectionString))
            {
                optionsBuilder.UseSqlServer(EnvironmentProperties.ConnectionString);
                return optionsBuilder;
            }

            optionsBuilder.UseInMemoryDatabase(EnvironmentProperties.DatabaseName);
            return optionsBuilder;
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(entity => entity.Relational().TableName = entity.DisplayName());
        }

        public static void RemoveCascadeDeleteBehavior(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys())
                .Where(w => !w.IsOwnership && w.DeleteBehavior.Equals(DeleteBehavior.Cascade)).ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }

        public static void SetStringColumnType(this ModelBuilder modelBuilder, int length)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetProperties()).Where(w => w.ClrType == typeof(string)).ToList()
                .ForEach(property =>
                {
                    property.Relational().ColumnType = "varchar";
                    property.AsProperty().Builder.HasMaxLength(length, ConfigurationSource.DataAnnotation);
                });
        }

        public static void SetDateTimeColumnType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetProperties()).Where(w => w.ClrType == typeof(DateTime)).ToList()
                .ForEach(property => property.Relational().ColumnType = "datetime2");
        }
    }
}