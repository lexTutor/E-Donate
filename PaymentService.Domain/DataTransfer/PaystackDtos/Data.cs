using Newtonsoft.Json;

namespace PaymentService.Domain.DataTransfer
{
    public class Data
    {
        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }
        [JsonProperty("access_code")]
        public string AccessCode { get; set; }
        public string Reference { get; set; }

    }
}