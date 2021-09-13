using Newtonsoft.Json;

namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class FlutterPaymentData
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}