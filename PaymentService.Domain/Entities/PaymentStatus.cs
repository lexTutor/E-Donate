using PaymentService.Domain.Common;

namespace PaymentService.Domain.Entities
{
    public class PaymentStatus : BaseEntity
    {
        public PaymentStatusType Status { get; set; }
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}