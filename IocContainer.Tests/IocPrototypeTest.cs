using NUnit.Framework;

namespace IocContainer.Tests
{
    [TestFixture]
    public class IocPrototypeTest : IocTestHelper
    {
        [Test]
        public void IocPrototypeDefinition()
        {
            Ioc.AddPrototypeDefinition<B>();

            var b1 = Ioc.Get<B>();
            var b2 = Ioc.Get<B>();

            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);
            Assert.AreNotSame(b1, b2);
        }

        [Test]
        public void IocPrototypeDefinition_WithADependency()
        {
            Ioc.AddPrototypeDefinition<B>();
            Ioc.AddPrototypeDefinition<C>();

            var c1 = Ioc.Get<C>();
            var c2 = Ioc.Get<C>();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.AreNotSame(c1, c2);

            Assert.IsNotNull(c1.B);
            Assert.IsNotNull(c2.B);
            Assert.AreNotSame(c1.B, c2.B);
        }

        [Test]
        public void IocPrototypeDefinition_WithMultipleDependencies()
        {
            Ioc.AddPrototypeDefinition<A>();
            Ioc.AddPrototypeDefinition<B>();
            Ioc.AddPrototypeDefinition<D>();

            var d1 = Ioc.Get<D>();
            var d2 = Ioc.Get<D>();

            Assert.IsNotNull(d1);
            Assert.IsNotNull(d2);
            Assert.AreNotSame(d1, d2);

            Assert.IsNotNull(d1.A);
            Assert.IsNotNull(d2.A);
            Assert.AreNotSame(d1.A, d2.A);

            Assert.IsNotNull(d1.B);
            Assert.IsNotNull(d2.B);
            Assert.AreNotSame(d1.B, d2.B);
        }

        [Test]
        public void IocPrototypeDefinition_CanRebindToItsSelf_DoesNotThrow()
        {
            Ioc.AddPrototypeDefinition<B>();
            Ioc.AddPrototypeDefinition<B>();

            Ioc.Get<B>();
        }

        [Test]
        public void IocPrototypeDefinition_InterfaceWithImplementation()
        {
            Ioc.AddPrototypeDefinition<IA, A>();

            var a1 = Ioc.Get<IA>();
            var a2 = Ioc.Get<IA>();

            Assert.IsNotNull(a1);
            Assert.IsNotNull(a2);
            Assert.AreNotSame(a1, a2);
            Assert.IsInstanceOf<A>(a1);
            Assert.IsInstanceOf<A>(a2);
        }

        [Test]
        public void IocPrototypeDefinition_InterfaceWithImplementationAndDependency()
        {
            Ioc.AddPrototypeDefinition<B>();
            Ioc.AddPrototypeDefinition<IA, C>();

            var a1 = Ioc.Get<IA>();
            var a2 = Ioc.Get<IA>();

            Assert.IsNotNull(a1);
            Assert.IsNotNull(a2);
            Assert.AreNotSame(a1, a2);
            Assert.IsInstanceOf<C>(a1);
            Assert.IsInstanceOf<C>(a2);

            var c1 = a1 as C;
            var c2 = a2 as C;

            Assert.IsNotNull(c1.B);
            Assert.IsNotNull(c2.B);
            Assert.AreNotSame(c1.B, c2.B);
        }

        [Test]
        public void IocPrototypeDefinition_CanRebindInterfaceToSameImplementation_DoesNotThrow()
        {
            Ioc.AddPrototypeDefinition<IA, A>();
            Ioc.AddPrototypeDefinition<IA, A>();

            Ioc.Get<IA>();
        }

        [Test]
        public void IocPrototypeDefinition_CanRebindInterfaceToDifferentImplementation_DoesNotThrow()
        {
            Ioc.AddPrototypeDefinition<IA, A>();
            Ioc.AddPrototypeDefinition<IA, C>();

            var a = Ioc.Get<IA>();
            Assert.IsInstanceOf<C>(a);
        }
    }
}