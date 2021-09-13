using AutoMapper;
using PaymentService.Application.Commons.Mapping;
using PaymentService.Application.Contracts;
using PaymentService.Infrastructure.SignedContracts;
using PaymentService.Persistence;
using PaymentService.Persistence.Repositories;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Extensions.Configuration;
using Unity;
using Unity.WebApi;
using System.Configuration;

namespace PaymentService.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<PaymentDbContext>();
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.RegisterType<IPaymentService, PaymentServiceExecutor>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IRequestHandler, HttpClientRequestHandler>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            container.RegisterInstance<IMapper>(config.CreateMapper());
            container.RegisterType<HttpClient>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}