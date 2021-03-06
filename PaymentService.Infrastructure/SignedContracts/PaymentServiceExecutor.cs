using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.DataTransfer.FlutterwaveDtos;
using PaymentService.Domain.DataTransfer.PaystackDtos;
using PaymentService.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace PaymentService.Infrastructure.SignedContracts
{
   public class PaymentServiceExecutor : IPaymentService
   {
      private readonly IRequestHandler _requestHandler;
      private readonly IBaseRepository<Payment> _paymentRepository;
      private readonly IMapper _mapper;
      private readonly IUnitOfWork _UOW;

      public PaymentServiceExecutor(IUnityContainer serviceProvider, IMapper mapper)
      {

         _requestHandler = (IRequestHandler)serviceProvider.Resolve(typeof(IRequestHandler))
            ?? throw new ArgumentNullException(nameof(_requestHandler));


         _UOW = (IUnitOfWork)serviceProvider.Resolve(typeof(IUnitOfWork))
             ?? throw new ArgumentNullException(nameof(_UOW));

         _paymentRepository = _UOW.PaymentRepository;

         _mapper = mapper;
      }
      public async Task<Response<PaymentDetailsResult>> GetPaymentDetails(string paymentId)
      {
         Response<PaymentDetailsResult> response = new Response<PaymentDetailsResult>();
         PaymentDetailsResult result = _mapper.Map<PaymentDetailsResult>(await _paymentRepository.GetByIdAsync(paymentId));

         response.Data = result;
         response.IsSuccess = result != null;
         response.Message = result is null ? "Resource does not exist" : "Successful";

         return response;
      }

      public async Task<Response<IEnumerable<PaymentHistoryDTO>>> GetPaymentHistory()
      {
         Response<IEnumerable<PaymentHistoryDTO>> response = new Response<IEnumerable<PaymentHistoryDTO>>();
         response.Data = _mapper.Map<IEnumerable<PaymentHistoryDTO>>(_UOW.PaymentRepository.Get());
         response.IsSuccess = true;
         response.Message = "All payment details";

         return response;
      }

      public async Task<Response<PaymentResultDTO>> MakePaymentWithFlutterWave(RecievePaymentDto model)
      {

         Response<PaymentResultDTO> response = new Response<PaymentResultDTO>();
         FlutterRequestDto flutterModel = _mapper.Map<FlutterRequestDto>(model);
         flutterModel.Customer = new Customer
         {
            Email = model.Email
         };
         string url = Path.Combine(ConfigurationManager.AppSettings["FlutterWave:Url"], "payments");
         FlutterResponseDto result = await _requestHandler.SendAsync<FlutterRequestDto, FlutterResponseDto>(flutterModel, url,
             ConfigurationManager.AppSettings["FlutterWave:Token"]);

         if(result.Data is null || string.IsNullOrWhiteSpace(result.Data.Link))
         {
            response.Message = "Unable to create payment intent at this time";
            return response;
         }

         response.IsSuccess = true;
         response.Message = "Payment intent created successfully";
         response.Data = await SavePaymentDetails(result.Data.Link, flutterModel.Reference, flutterModel.Amount, "flutter_wave", model.Email);

         return response;
      }

      public async Task<Response<PaymentResultDTO>> CreatePaystackPaymentIntent(RecievePaymentDto model)
      {
         Response<PaymentResultDTO> response = new Response<PaymentResultDTO>();
         PaystackRequestDto payStackMap = _mapper.Map<PaystackRequestDto>(model);
         string url = Path.Combine(ConfigurationManager.AppSettings["Paystack:Url"], "initialize");
         PaystackReturnDto result = await _requestHandler.SendAsync<PaystackRequestDto, PaystackReturnDto>(payStackMap, url,
                ConfigurationManager.AppSettings["Paystack:Token"]);

         if(result.Data is null || string.IsNullOrWhiteSpace(result.Data.AuthorizationUrl))
         {
            response.Message = "Unable to create payment intent at this time";
            return response;
         }


         response.IsSuccess = true;
         response.Message = "Payment intent created successfully";

         response.Data = await SavePaymentDetails(result.Data.AuthorizationUrl, result.Data.Reference, payStackMap.Amount, "paystack", model.Email);
         return response;
      }

      public async Task<Response<PaymentDetailsResult>> VerifyPaystackPayment(string paymentId)
      {
         Response<PaymentDetailsResult> response = new Response<PaymentDetailsResult>();

         Payment payment = await _paymentRepository.GetByIdAsync(paymentId);
         if(payment is null)
         {
            response.Message = "No payment to verify";
            return response;
         }

         string url = Path.Combine(ConfigurationManager.AppSettings["Paystack:Url"], "verify/" + payment.Reference);
         BasicResponse result = await _requestHandler.GetAsync<BasicResponse>(url, ConfigurationManager.AppSettings["Paystack:Token"]);

         if(result.IsSuccessful)
         {
            payment.PaymentStatus = PaymentStatusType.Processed;
            await _UOW.SaveChangesAsync();

            response = await GetPaymentDetails(paymentId);
            response.Message = "Payment verified";
            return response;
         }

         payment.PaymentStatus = PaymentStatusType.Failed;

         await _UOW.SaveChangesAsync();
         response = await GetPaymentDetails(paymentId);
         response.Message = "Payment was not successful";
         return response;
      }

      public async Task PaymentRequestSuccessfulEvent(object model)
      {
         String key = "YOUR_SECRET_KEY"; //replace with your paystack secret_key
         String jsonInput = JsonConvert.SerializeObject(model); //the json input
         String inputString = Convert.ToString(new JValue(jsonInput));
         String result = "";

         byte[] secretkeyBytes = Encoding.UTF8.GetBytes(key);
         byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
         using(var hmac = new HMACSHA512(secretkeyBytes))
         {
            byte[] hashValue = hmac.ComputeHash(inputBytes);
            result = BitConverter.ToString(hashValue).Replace("-", string.Empty);
            ;
         }


         String xpaystackSignature = "X-Paystack-Signature";
         if(result.ToLower().Equals(xpaystackSignature))
         {
            PaymentReceivedEventDto eventData = JsonConvert.DeserializeObject<PaymentReceivedEventDto>(jsonInput);
            Payment payment = await _paymentRepository.FindAsync(p => p.Reference == eventData.Reference);

            payment.PaymentStatus = PaymentStatusType.Processed;
            payment.Paid_At = eventData.Paid_At;

            await _UOW.SaveChangesAsync();
            return;
         }

         return;
      }

      private async Task<PaymentResultDTO> SavePaymentDetails(string link, string reference, decimal amount, string client, string email)
      {
         Payment payment = new Payment
         {
            Amount = amount,
            Reference = reference,
            PaymentStatus = PaymentStatusType.Pending,
            Paid_At = DateTime.Now,
            Email = email,
            PaymentMethod = client
         };

         _paymentRepository.Add(payment);

         var response = new PaymentResultDTO
         {
            PaymentId = payment.Id,
            PaymentStatus = payment.PaymentStatus.ToString(),
            PaymentUrl = link
         };

         await _UOW.SaveChangesAsync();

         return response;

      }

      public async Task<Response<PaymentDetailsResult>> VerifyPayment(string paymentId, string providerName)
      {
         if(providerName.ToLower() == "paystack")
            return await VerifyPaystackPayment(paymentId);
         if(providerName.ToLower() == "flutterwave")
            return await VerifyFlutterWavePayment(paymentId);
         return new Response<PaymentDetailsResult>
         {
            Message = "Unknown Provider. Must be either Paystack or FlutterWave. Please ensure your spelling is correct"
         };
      }

      private async Task<Response<PaymentDetailsResult>> VerifyFlutterWavePayment(string paymentId)
      {
         Response<PaymentDetailsResult> response = new Response<PaymentDetailsResult>();

         Payment payment = await _paymentRepository.GetByIdAsync(paymentId);
         if(payment is null)
         {
            response.Message = "No payment to verify";
            return response;
         }
         string transactId = string.Empty;

         if(!payment.Reference.Contains(","))
         {
            response.Message = "Payment has not been made";
            return response;
         }

         transactId = payment.Reference.Split(',')[1];

         string tranPath = "transactions/" + transactId + "/verify";
         string url = Path.Combine(ConfigurationManager.AppSettings["FlutterWave:Url"], tranPath);

         FlutterVerificationResponseDto result = await _requestHandler.GetAsync<FlutterVerificationResponseDto>(url,
                ConfigurationManager.AppSettings["FlutterWave:Token"]);

         if(result.Data.Status == "successful" && result.Data.Amount == payment.Amount)
         {
            payment.PaymentStatus = PaymentStatusType.Processed;
            await _UOW.SaveChangesAsync();

            response = await GetPaymentDetails(paymentId);
            response.Message = "Payment verified";
            return response;
         }

         payment.PaymentStatus = PaymentStatusType.Failed;

         await _UOW.SaveChangesAsync();
         response = await GetPaymentDetails(paymentId);
         response.Message = "Payment was not successful";
         return response;
      }

      public async Task<bool> UpdateFlutterPayment(string transactionId, string refernce)
      {
         Payment payment = await _paymentRepository.FindAsync(p => p.Reference == refernce);

         payment.Reference = refernce + "," + transactionId;

         payment.PaymentStatus = PaymentStatusType.Processed;

         return await _UOW.SaveChangesAsync();
      }
   }
}
