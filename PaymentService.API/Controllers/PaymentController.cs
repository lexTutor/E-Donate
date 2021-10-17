using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PaymentService.API.Controllers
{
   [RoutePrefix("api/v1/Payment")]
   public class PaymentController : ApiController
   {
      private readonly IPaymentService _paymentService;
      public PaymentController() : base()
      {

      }
      public PaymentController(IPaymentService paymentService)
      {
         _paymentService = paymentService;
      }

      [HttpGet]
      [Route("get-all")]
      public async Task<HttpResponseMessage> GetPaymentHistory()
      {
         Response<IEnumerable<PaymentHistoryDTO>> result = await _paymentService.GetPaymentHistory();
         return !result.IsSuccess
                ? Request.CreateResponse(HttpStatusCode.BadRequest, result)
                : Request.CreateResponse(HttpStatusCode.OK, result);
      }

      [HttpPost]
      [Route("paystack")]
      public async Task<HttpResponseMessage> CreatePayStacksPaymentIntent([FromBody] RecievePaymentDto model)
      {
         Response<PaymentResultDTO> result = await _paymentService.CreatePaystackPaymentIntent(model);
         return !result.IsSuccess 
                ? Request.CreateResponse(HttpStatusCode.BadRequest, result) 
                : Request.CreateResponse(HttpStatusCode.OK, result);
      }

      [HttpPost]
      [Route("flutterwave")]
      public async Task<HttpResponseMessage> CreateFlutterWavePaymentIntent([FromBody] RecievePaymentDto model)
      {
         Response<PaymentResultDTO> result = await _paymentService.MakePaymentWithFlutterWave(model);
         return !result.IsSuccess
               ? Request.CreateResponse(HttpStatusCode.BadRequest, result)
               : Request.CreateResponse(HttpStatusCode.OK, result);
      }

      [Route("EventHook")]
      public async Task<HttpResponseMessage> PaymentSuccessfulEvent([FromBody] object model)
      {
         await _paymentService.PaymentRequestSuccessfulEvent(model);
         return Request.CreateResponse(HttpStatusCode.OK);
      }

      [HttpGet]
      [Route("flutter-confirm")]
      public async Task<HttpResponseMessage> FlutterConfirm([FromUri] string tx_ref, [FromUri] string transaction_id)
      {
         bool result = await _paymentService.UpdateFlutterPayment(transaction_id, tx_ref);
         return result ? Request.CreateResponse(HttpStatusCode.OK) :
                        Request.CreateResponse(HttpStatusCode.BadRequest);
      }

      [HttpGet]
      public async Task<HttpResponseMessage> GetPaymentDetails([FromUri] string paymentId)
      {
         Response<PaymentDetailsResult> result = await _paymentService.GetPaymentDetails(paymentId);
         return !result.IsSuccess 
                ? Request.CreateResponse(HttpStatusCode.BadRequest, result) 
                : Request.CreateResponse(HttpStatusCode.OK, result);
      }

      [Route("Verify")]
      [HttpGet]
      public async Task<HttpResponseMessage> VerifyPayment([FromUri] string paymentId, [FromUri] string providerName)
      {
         Response<PaymentDetailsResult> result = await _paymentService.VerifyPayment(paymentId, providerName);
         return !result.IsSuccess 
                ? Request.CreateResponse(HttpStatusCode.BadRequest, result) 
                : Request.CreateResponse(HttpStatusCode.OK, result);
      }
   }
}