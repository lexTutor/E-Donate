using Microsoft.AspNet.Identity.EntityFramework;
using PaymentService.Domain.Entities;
using PaymentService.Persistence.EntityConfigurations;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentService.Persistence
{
    public class PaymentDbContext : IdentityDbContext<AppUser>
    {
        public PaymentDbContext() :
            base("PaymentDatabase", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Payment> Payments { get; set; }
        //public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        public static PaymentDbContext Create()
        {
            return new PaymentDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Applies the entity configuration for the Payment model
            builder.Configurations.Add(new EntityConfigurationsForPayment());

            //Applies the entity configurations for the Payment Status model.
            //builder.Configurations.Add(new EntityConfigurationsForPaymentStatus());

            //This is to convert any decimal in the models to doubles as sqlite does not support decimal values.
            if (Database.GetType().Name == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                var type = builder.Entity<Payment>().GetType().GetProperty("Amount");
                builder.Entity<Payment>().Property(X => X.Amount).HasColumnType("Double");
            }
        }

        //Overrides the SaveChangesAsync method to enable setting the created and updatedAt fields as required
        public override async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(entry => entry.Entity is BaseEntity && (
                        entry.State == EntityState.Added
                        || entry.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
