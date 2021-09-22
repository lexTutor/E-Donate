using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PaymentService.API.StartUp))]

namespace PaymentService.API
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}