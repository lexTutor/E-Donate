using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer
{
    public class PaystackRequestDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("amount")]
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value * 100; }
        }

        [JsonProperty("callback_url")]
        public string Callback_url { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; } = Guid.NewGuid().ToString();
    }
}
