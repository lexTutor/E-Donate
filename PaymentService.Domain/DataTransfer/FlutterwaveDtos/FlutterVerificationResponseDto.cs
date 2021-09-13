using Newtonsoft.Json;
using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class FlutterVerificationResponseDto : BasicResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public FlutterVerificationData Data { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

    }
}
