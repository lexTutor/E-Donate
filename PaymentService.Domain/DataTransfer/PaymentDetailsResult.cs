using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.DataTransfer
{
    public class PaymentDetailsResult
    {
        public string Id { get; set; }
        public string PaymentStatus { get; set; }
    }
}
