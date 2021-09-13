
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace PaymentService.Persistence.EntityConfigurations
{
    public class EntityConfigurationsForPaymentStatus : EntityTypeConfiguration<PaymentStatus>
    {
        public EntityConfigurationsForPaymentStatus()
        {
            this.ToTable("PaymentStatus");
            //this.HasRequired(paymentStatus => paymentStatus.Payment)
            //    .WithRequiredDependent(Payment => Payment.PaymentStatus);
        }
    }
}
