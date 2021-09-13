using Newtonsoft.Json;
using System;

namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class FlutterVerificationData
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created_At { get; set; }
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
    }
}