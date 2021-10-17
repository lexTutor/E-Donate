using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
   public interface IPaymentService
   {
      Task<Response<PaymentResultDTO>> CreatePaystackPaymentIntent(RecievePaymentDto model);
      Task PaymentRequestSuccessfulEvent(object model);
      Task<Response<PaymentResultDTO>> MakePaymentWithFlutterWave(RecievePaymentDto model);
      Task<Response<PaymentDetailsResult>> VerifyPayment(string paymentId, string providerName);
      Task<Response<PaymentDetailsResult>> GetPaymentDetails(string paymentId);
      Task<Response<IEnumerable<PaymentHistoryDTO>>> GetPaymentHistory();
      Task<bool> UpdateFlutterPayment(string transactionId, string refernce);
   }
}
