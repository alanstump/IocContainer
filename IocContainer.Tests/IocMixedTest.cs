using NUnit.Framework;

namespace IocContainer.Tests
{
    [TestFixture]
    public class IocMixedTest : IocTestHelper
    {
        [Test]
        public void SingletonWithPrototypeDependency()
        {
            Ioc.AddPrototypeDefinition<B>();
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
        public void SingletonWithPrototypeAndSingletonDependencies()
        {
            Ioc.AddPrototypeDefinition<A>();
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
        public void PrototypeWithSingletonDependency()
        {
            Ioc.AddSingletonDefinition<B>();
            Ioc.AddPrototypeDefinition<C>();

            var c1 = Ioc.Get<C>();
            var c2 = Ioc.Get<C>();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.AreNotSame(c1, c2);

            Assert.IsNotNull(c1.B);
            Assert.IsNotNull(c2.B);
            Assert.AreSame(c1.B, c2.B);
        }

        [Test]
        public void PrototypeWithSingletonAndPrototypeDependencies()
        {
            Ioc.AddSingletonDefinition<A>();
            Ioc.AddPrototypeDefinition<B>();
            Ioc.AddPrototypeDefinition<D>();

            var d1 = Ioc.Get<D>();
            var d2 = Ioc.Get<D>();

            Assert.IsNotNull(d1);
            Assert.IsNotNull(d2);
            Assert.AreNotSame(d1, d2);

            Assert.IsNotNull(d1.A);
            Assert.IsNotNull(d2.A);
            Assert.AreSame(d1.A, d2.A);

            Assert.IsNotNull(d1.B);
            Assert.IsNotNull(d2.B);
            Assert.AreNotSame(d1.B, d2.B);
        }
    }
}