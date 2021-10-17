using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Entities
{
   public class Payment : BaseEntity
   {
      public decimal Amount { get; set; }
      public string Reference { get; set; }
      //public string PaymentSatustId { get; set; }
      public DateTime Paid_At { get; set; }
      public string Email { get; set; }
      public string PaymentMethod { get; set; }
      public PaymentStatusType PaymentStatus { get; set; }
   }
}
