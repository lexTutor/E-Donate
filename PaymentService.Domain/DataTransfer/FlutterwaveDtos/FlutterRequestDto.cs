using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class FlutterRequestDto
    {
        public FlutterRequestDto()
        {
            Reference = "E-DN_tx" + Guid.NewGuid().ToString("N").Substring(4, 15);
        }
        [JsonProperty("tx_ref")]
        public string Reference { get; set; }
        private decimal amount;
        [JsonProperty("amount")]
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        [JsonProperty("currency")]
        public string Currency { get; set; } = "NGN";

        [JsonProperty("redirect_url")]
        public string Callback_Url { get; set; }
        [JsonProperty("payment_options")]
        public string PaymentOptions { get; set; }
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        public Customizations Customizations { get; set; }
    }
}
