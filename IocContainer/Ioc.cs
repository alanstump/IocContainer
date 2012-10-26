using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocContainer
{
    public static class Ioc
    {
        static IKernel _standardKernel;

        public Ioc()
        {
            _standardKernel = new StandardKernel();
        }
    }
}
