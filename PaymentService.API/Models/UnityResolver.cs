using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace PaymentService.API.Models
{
    public class UnityResolver2 : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver2(IUnityContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }
        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver2(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {

                throw new InvalidOperationException($"Unable to resolve sertvice of type {serviceType}", ex);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException ex)
            {

                throw new InvalidOperationException($"Unable to resolve sertvice of type {serviceType}", ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}