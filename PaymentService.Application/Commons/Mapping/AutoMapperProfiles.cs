using AutoMapper;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.DataTransfer.AccountDtos;
using PaymentService.Domain.DataTransfer.FlutterwaveDtos;
using PaymentService.Domain.Entities;

namespace PaymentService.Application.Commons.Mapping
{
   public class AutoMapperProfiles : Profile
   {
      public AutoMapperProfiles()
      {
         //source => destination
         CreateMap<RecievePaymentDto, Payment>();
         CreateMap<Payment, PaymentResultDTO>();
         CreateMap<Payment, PaymentDetailsResult>();
         CreateMap<RecievePaymentDto, PaystackRequestDto>();
         CreateMap<RecievePaymentDto, FlutterRequestDto>();
         CreateMap<RegisterationRequestDto, AppUser>();
         CreateMap<PaystackReturnDto, PaymentResultDTO>();
         CreateMap<AppUser, UserResponseDto>();
         CreateMap<Payment, PaymentHistoryDTO>()
            .ForMember(ph => ph.PaymentStatus, p => p.MapFrom(r => r.PaymentStatus.ToString()))
            .ForMember(ph => ph.PaidAt, p => p.MapFrom(r => r.Paid_At))
            .ForMember(ph => ph.Client, p => p.MapFrom(r => r.PaymentMethod));
      }
   }
}
