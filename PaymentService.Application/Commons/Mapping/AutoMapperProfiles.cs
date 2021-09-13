using AutoMapper;
using PaymentService.Domain.DataTransfer;
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
        }
    }
}
