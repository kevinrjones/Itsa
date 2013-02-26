// grabbed from https://github.com/pszmyd/SignalR.Autofac

using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Microsoft.AspNet.SignalR;
using Autofac;
using Autofac.Builder;

namespace ItsaWeb.Infrastructure
{
    /// <summary>
    /// SingalR dependency resolver using Autofac container as backend.
    /// </summary>
    public class AutofacDependencyResolver : DefaultDependencyResolver, IRegistrationSource
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _lifetimeScope.ComponentRegistry.AddRegistrationSource(this);
        }

        public override object GetService(Type serviceType)
        {
            //            object result = AutofacDependencyResolver.Current.GetService(serviceType);
            object result;
            if (_lifetimeScope.TryResolve(serviceType, out result))
            {
                return result;
            }

            return null;
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            object result;
            if (_lifetimeScope.TryResolve(typeof(IEnumerable<>).MakeGenericType(serviceType), out result))
            {
                return (IEnumerable<object>)result;
            }

            return Enumerable.Empty<object>();
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var typedService = service as TypedService;
            if (typedService != null)
            {
                var instances = base.GetServices(typedService.ServiceType);

                if (instances != null)
                {
                    return instances
                            .Select(i => RegistrationBuilder.ForDelegate(i.GetType(), (c, p) => i).As(typedService.ServiceType)
                            .InstancePerMatchingLifetimeScope(_lifetimeScope.Tag)
                            .PreserveExistingDefaults()
                            .CreateRegistration());
                }
            }

            return Enumerable.Empty<IComponentRegistration>();
        }

        bool IRegistrationSource.IsAdapterForIndividualComponents
        {
            get { return false; }
        }
    }
}