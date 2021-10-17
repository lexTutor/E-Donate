using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PaymentService.Domain.DataTransfer
{
   public class PaymentHistoryDTO
   {
      [JsonProperty("Id")]
      public string Id { get; set; }
      [JsonProperty("paymentMethod")]
      public string Client { get; set; }
      [JsonProperty("email")]
      public string Email { get; set; }
      [JsonProperty("paidAt")]
      public DateTime PaidAt { get; set; }
      [JsonProperty("amount")]
      public decimal Amount { get; set; }
      [JsonProperty("createdAt")]
      public DateTime CreatedAt { get; set; }
      [JsonProperty("paymentStatus")]
      public string PaymentStatus { get; set; }
   }
}
