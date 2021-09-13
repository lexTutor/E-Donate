using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer.PaystackDtos
{ 
    public class PaymentReceivedEventDto
    {
        [JsonProperty("paid_at")]
        public DateTime Paid_At { get; set; }

        [JsonProperty("offline_reference")]
        public string Reference { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
