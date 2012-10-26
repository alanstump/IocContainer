using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocContainer
{
    public static class Ioc
    {
        internal static StandardKernel _standardKernel;

        static Ioc()
        {
            _standardKernel = new StandardKernel();
        }

        public static void AddSingletonDefinition<T>()
        {
            if (CanResolve<T>())
                _standardKernel.Rebind<T>().ToSelf().InSingletonScope();
            else
                _standardKernel.Bind<T>().ToSelf().InSingletonScope();
        }

        public static void AddSingletonDefinition<TInt, TImp>() where TImp : TInt
        {
            if (CanResolve<TInt>())
                _standardKernel.Rebind<TInt>().To<TImp>().InSingletonScope();
            else            
                _standardKernel.Bind<TInt>().To<TImp>().InSingletonScope();
        }

        public static void AddPrototypeDefinition<T>()
        {
            if (CanResolve<T>())
                _standardKernel.Rebind<T>().ToSelf();
            else
                _standardKernel.Bind<T>().ToSelf();
        }

        public static void AddPrototypeDefinition<TInt, TImp>() where TImp : TInt
        {
            if (CanResolve<TInt>())
                _standardKernel.Rebind<TInt>().To<TImp>();
            else
                _standardKernel.Bind<TInt>().To<TImp>();
        }

        public static T Get<T>()
        {
            return _standardKernel.Get<T>();
        }

        static bool CanResolve<T>()
        {
            var request = _standardKernel.CreateRequest(typeof(T), x => true, new List<IParameter>(), false, false);
            return _standardKernel.CanResolve(request);
        }
    }
}
