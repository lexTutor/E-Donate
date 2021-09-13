using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class FlutterResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]

        public string Message { get; set; }
        [JsonProperty("data")]
        public FlutterPaymentData Data { get; set; }
    }
}
