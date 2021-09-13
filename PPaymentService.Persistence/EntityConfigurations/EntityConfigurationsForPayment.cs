
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace PaymentService.Persistence.EntityConfigurations
{
    public class EntityConfigurationsForPayment : EntityTypeConfiguration<Payment>
    {
        public EntityConfigurationsForPayment()
        {
            this.ToTable("Payment");
            this.Property(p => p.Paid_At).IsOptional();
            //this.HasOptional(payment => payment.PaymentStatus)
            //    .WithRequired(PaymentStatus => PaymentStatus.Payment)
            //    .WillCascadeOnDelete();
        }
    }
}
