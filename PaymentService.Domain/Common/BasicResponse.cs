using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Common
{
    public class BasicResponse
    {
        [JsonProperty("status")]
        public bool IsSuccessful { get; set; }
    }
}
