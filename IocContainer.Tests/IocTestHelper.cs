namespace IocContainer.Tests
{
    public class IocTestHelper
    {
        public interface IA { }
        public class A : IA { }
        public class B { }

        public class C : IA
        {
            public B B;

            public C(B b)
            {
                B = b;
            }
        }

        public class D
        {
            public A A;
            public B B;

            public D(A a, B b)
            {
                A = a;
                B = b;
            }
        } 
    }
}