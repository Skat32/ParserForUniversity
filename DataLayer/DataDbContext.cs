using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Models.Entities;

namespace DataLayer
{
    public class DataDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
        
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
        
        public static DataDbContext CreateDbContext(string connectionStrings)
        {
            var optionBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionBuilder.UseNpgsql(connectionStrings);

            return new DataDbContext(optionBuilder.Options);
        }
        
        #region | Override methods |
        
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            UpdateDate(ChangeTracker);
        
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            UpdateDate(ChangeTracker);

            return base.SaveChangesAsync(cancellationToken);
        }
        
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateDate(ChangeTracker);

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        
        public override int SaveChanges()
        {
            UpdateDate(ChangeTracker);

            return base.SaveChanges();
        }
        
        #endregion
     
        #region | Private methods |

        private static void UpdateDate(ChangeTracker changeTracker)
        {
            var dateTimeNow = DateTime.UtcNow;

            var dateTime = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour,
                dateTimeNow.Minute, dateTimeNow.Second, dateTimeNow.Millisecond);

            foreach (var entityEntry in changeTracker.Entries())
            {
                if (entityEntry.Entity is IUpdatableEntity entity)
                {
                    entity.UpdatedAt = dateTime;

                    if (entityEntry.State == EntityState.Added && entity.CreatedAt == default)
                        entity.CreatedAt = dateTime;
                }

                if (entityEntry.State == EntityState.Deleted 
                    && entityEntry.Entity is ISoftDeletableEntity entityDeleted)
                {
                    entityDeleted.IsDeleted = true;
                    entityEntry.State = EntityState.Modified;
                }
            }
        }

        #endregion
    }
}