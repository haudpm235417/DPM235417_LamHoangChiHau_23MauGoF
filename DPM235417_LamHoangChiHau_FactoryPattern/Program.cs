using System;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Creator
    abstract class Creator
    {
        public abstract IProduct FactoryMethod();

        public string SomeOperation()
        {
            // Gọi Factory Method để tạo Product
            var product = FactoryMethod();

            // Sử dụng Product
            var result = "Creator: The same creator's code has just worked with "
                + product.Operation();

            return result;
        }
    }

    // Concrete Creator 1
    class ConcreteCreator1 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }

    // Concrete Creator 2
    class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

    // Product
    public interface IProduct
    {
        string Operation();
    }

    // Concrete Product 1
    class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    // Concrete Product 2
    class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }

    // Client
    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());

            Console.WriteLine();

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());
        }

        public void ClientCode(Creator creator)
        {
            Console.WriteLine(
                "Client: I'm not aware of the creator's class, " +
                "but it still works.\n" +
                creator.SomeOperation()
            );
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Khởi tạo Client và chạy chương trình
            new Client().Main();

            Console.ReadKey();
        }
    }
}