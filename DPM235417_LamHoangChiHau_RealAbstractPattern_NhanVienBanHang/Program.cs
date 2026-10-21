using System;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Abstract Factory
    public interface IAbstractFactory
    {
        IAbstractProductA CreateProductA();
        IAbstractProductB CreateProductB();
    }

    // Concrete Factory 1
    class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA1();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB1();
        }
    }

    // Concrete Factory 2
    class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA2();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB2();
        }
    }

    // Abstract Product A
    public interface IAbstractProductA
    {
        string UsefulFunctionA();
    }

    // Concrete Product A1
    class ConcreteProductA1 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    // Concrete Product A2
    class ConcreteProductA2 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A2.";
        }
    }

    // Abstract Product B
    public interface IAbstractProductB
    {
        string UsefulFunctionB();

        string AnotherUsefulFunctionB(IAbstractProductA collaborator);
    }

    // Concrete Product B1
    class ConcreteProductB1 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B1.";
        }

        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }

    // Concrete Product B2
    class ConcreteProductB2 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B2.";
        }

        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B2 collaborating with the ({result})";
        }
    }

    // Client
    class Client
    {
        public void Main()
        {
            Console.WriteLine(
                "Client: Testing client code with the first factory type..."
            );

            ClientMethod(new ConcreteFactory1());

            Console.WriteLine();

            Console.WriteLine(
                "Client: Testing the same client code with the second factory type..."
            );

            ClientMethod(new ConcreteFactory2());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            // Tạo Product A và Product B
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            // Sử dụng Product B
            Console.WriteLine(productB.UsefulFunctionB());

            // Product B tương tác với Product A
            Console.WriteLine(
                productB.AnotherUsefulFunctionB(productA)
            );
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Khởi chạy Client
            new Client().Main();

            Console.ReadKey();
        }
    }
}