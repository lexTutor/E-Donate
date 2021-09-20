using Newtonsoft.Json;
using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.DataTransfer
{
    public class PaymentResultDTO
    {
        [JsonProperty("paymentId")]
        public string PaymentId { get; set; }
        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }
        [JsonProperty("AuthorizationUrl")]
        public string PaymentUrl { get; set; }
    }
}
