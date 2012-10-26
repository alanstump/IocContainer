using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace IocContainer
{
    public class IocResolver : IDependencyResolver
    {
        readonly IKernel _kernel;

        public IocResolver()
        {
            _kernel = Ioc.StandardKernel;
        }

        public object GetService(Type serviceType)
        {
            var request = _kernel.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return _kernel.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var request = _kernel.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return _kernel.Resolve(request).ToList();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}