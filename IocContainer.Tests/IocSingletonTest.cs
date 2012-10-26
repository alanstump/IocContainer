using IocContainer;
using NUnit.Framework;

namespace IocContainer.Tests
{
    [TestFixture]
    public class IocSingletonTest : IocTestHelper
    {
        [Test]
        public void IocSingletonDefinition()
        {
            Ioc.AddSingletonDefinition<B>();

            var b1 = Ioc.Get<B>();
            var b2 = Ioc.Get<B>();

            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);
            Assert.AreSame(b1, b2);
        }
        
        [Test]
        public void IocSingletonDefinition_WithADependency()
        {
            Ioc.AddSingletonDefinition<B>();
            Ioc.AddSingletonDefinition<C>();

            var c1 = Ioc.Get<C>();
            var c2 = Ioc.Get<C>();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.AreSame(c1, c2);

            Assert.IsNotNull(c1.B);
            Assert.IsNotNull(c2.B);
            Assert.AreSame(c1.B, c2.B);
        }
        
        [Test]
        public void IocSingletonDefinition_WithMultipleDependencies()
        {
            Ioc.AddSingletonDefinition<A>();
            Ioc.AddSingletonDefinition<B>();
            Ioc.AddSingletonDefinition<D>();

            var d1 = Ioc.Get<D>();
            var d2 = Ioc.Get<D>();

            Assert.IsNotNull(d1);
            Assert.IsNotNull(d2);
            Assert.AreSame(d1, d2);

            Assert.IsNotNull(d1.A);
            Assert.IsNotNull(d2.A);
            Assert.AreSame(d1.A, d2.A);

            Assert.IsNotNull(d1.B);
            Assert.IsNotNull(d2.B);
            Assert.AreSame(d1.B, d2.B);
        }

        [Test]
        public void IocSingletonDefinition_CanRebindToItsSelf_DoesNotThrow()
        {
            Ioc.AddSingletonDefinition<B>();
            Ioc.AddSingletonDefinition<B>();

            Ioc.Get<B>();
        }

        [Test]
        public void IocSingletonDefinition_InterfaceWithImplementation()
        {
            Ioc.AddSingletonDefinition<IA, A>();

            var a1 = Ioc.Get<IA>();
            var a2 = Ioc.Get<IA>();

            Assert.IsNotNull(a1);
            Assert.IsNotNull(a2);
            Assert.AreSame(a1, a2);
            Assert.IsInstanceOf<A>(a1);
            Assert.IsInstanceOf<A>(a2);
        }
        
        [Test]
        public void IocSingletonDefinition_InterfaceWithImplementationAndDependency()
        {
            Ioc.AddSingletonDefinition<B>();
            Ioc.AddSingletonDefinition<IA, C>();

            var a1 = Ioc.Get<IA>();
            var a2 = Ioc.Get<IA>();

            Assert.IsNotNull(a1);
            Assert.IsNotNull(a2);
            Assert.AreSame(a1, a2);
            Assert.IsInstanceOf<C>(a1);
            Assert.IsInstanceOf<C>(a2);

            var c1 = a1 as C;
            var c2 = a2 as C;

            Assert.IsNotNull(c1.B);
            Assert.IsNotNull(c2.B);
            Assert.AreSame(c1.B, c2.B);
        }

        [Test]
        public void IocSingletonDefinition_CanRebindInterfaceToSameImplementation_DoesNotThrow()
        {
            Ioc.AddSingletonDefinition<IA, A>();
            Ioc.AddSingletonDefinition<IA, A>();

            Ioc.Get<IA>();
        }

        [Test]
        public void IocSingletonDefinition_CanRebindInterfaceToDifferentImplementation_DoesNotThrow()
        {
            Ioc.AddSingletonDefinition<IA, A>();
            Ioc.AddSingletonDefinition<IA, C>();

            var a = Ioc.Get<IA>();
            Assert.IsInstanceOf<C>(a);
        }
    }
}