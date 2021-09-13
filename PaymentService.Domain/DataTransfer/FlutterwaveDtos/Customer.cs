using Newtonsoft.Json;

namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class Customer
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}