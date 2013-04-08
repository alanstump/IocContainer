using Ninject;
using Ninject.Parameters;
using System.Collections.Generic;

namespace IocContainer
{
    public static class Ioc
    {
        internal static StandardKernel StandardKernel;

        static Ioc()
        {
            StandardKernel = new StandardKernel();
        }

        public static void AddSingletonDefinition<T>()
        {
            if (CanResolve<T>())
                StandardKernel.Rebind<T>().ToSelf().InSingletonScope();
            else
                StandardKernel.Bind<T>().ToSelf().InSingletonScope();
        }

        public static void AddSingletonDefinition<TInt, TImp>() where TImp : TInt
        {
            if (CanResolve<TInt>())
                StandardKernel.Rebind<TInt>().To<TImp>().InSingletonScope();
            else            
                StandardKernel.Bind<TInt>().To<TImp>().InSingletonScope();
        }

        public static void AddPrototypeDefinition<T>()
        {
            if (CanResolve<T>())
                StandardKernel.Rebind<T>().ToSelf();
            else
                StandardKernel.Bind<T>().ToSelf();
        }

        public static void AddPrototypeDefinition<TInt, TImp>() where TImp : TInt
        {
            if (CanResolve<TInt>())
                StandardKernel.Rebind<TInt>().To<TImp>();
            else
                StandardKernel.Bind<TInt>().To<TImp>();
        }

        public static T Get<T>()
        {
            return StandardKernel.Get<T>();
        }

        static bool CanResolve<T>()
        {
            var request = StandardKernel.CreateRequest(typeof(T), x => true, new List<IParameter>(), false, false);
            return StandardKernel.CanResolve(request);
        }
    }
}
