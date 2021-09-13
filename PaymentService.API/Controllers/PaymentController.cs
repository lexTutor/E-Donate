using PaymentService.Application.Contracts;
using PaymentService.Domain.DataTransfer;
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
        public PaymentController(): base()
        {

        }
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("paystack")]
        public async Task<HttpResponseMessage> CreatePayStacksPaymentIntent([FromBody]RecievePaymentDto model)
        {
          var result = await  _paymentService.CreatePaystackPaymentIntent(model);
            if (!result.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("flutterwave")]
        public async Task<HttpResponseMessage> CreateFlutterWavePaymentIntent([FromBody] RecievePaymentDto model)
        {
            var result = await _paymentService.MakePaymentWithFlutterWave(model);
            if (!result.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("EventHook")]
        public async Task<HttpResponseMessage> PaymentSuccessfulEvent([FromBody]object model)
        {
             await _paymentService.PaymentRequestSuccessfulEvent(model);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("flutter-confirm")]
        public async Task<HttpResponseMessage> FlutterConfirm([FromUri] string tx_ref, [FromUri] string transaction_id, [FromUri] string status)
        {
            var result = await  _paymentService.UpdateFlutterPayment(transaction_id, tx_ref);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetPaymentDetails([FromUri] string paymentId)
        {
            var result = await _paymentService.GetPaymentDetails(paymentId);
            if (!result.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Verify")]
        [HttpGet]
        public async Task<HttpResponseMessage> VerifyPayment([FromUri] string paymentId, [FromUri]string providerName)
        {
            var result = await _paymentService.VerifyPayment(paymentId, providerName);
            if (!result.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}