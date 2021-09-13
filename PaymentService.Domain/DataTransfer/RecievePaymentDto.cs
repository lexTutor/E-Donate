using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.DataTransfer
{
    public class RecievePaymentDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("callback_url")]
        public string Callback_Url { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
