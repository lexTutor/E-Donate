using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer
{
    public class PaystackReturnDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }
}
