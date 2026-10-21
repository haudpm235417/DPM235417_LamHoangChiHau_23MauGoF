using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Builder
    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
    }

    // Concrete Builder
    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new Product();

        public ConcreteBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._product = new Product();
        }

        public void BuildPartA()
        {
            this._product.Add("PartA1");
        }

        public void BuildPartB()
        {
            this._product.Add("PartB1");
        }

        public void BuildPartC()
        {
            this._product.Add("PartC1");
        }

        public Product GetProduct()
        {
            Product result = this._product;

            this.Reset();

            return result;
        }
    }

    // Product
    public class Product
    {
        private List<object> _parts = new List<object>();

        public void Add(string part)
        {
            this._parts.Add(part);
        }

        public string ListParts()
        {
            if (_parts.Count == 0)
            {
                return "Product parts: Không có thành phần.";
            }

            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            // Xóa dấu ", " cuối cùng
            str = str.Remove(str.Length - 2);

            return "Product parts: " + str;
        }
    }

    // Director
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        // Sản phẩm tối thiểu
        public void BuildMinimalViableProduct()
        {
            this._builder.BuildPartA();
        }

        // Sản phẩm đầy đủ
        public void BuildFullFeaturedProduct()
        {
            this._builder.BuildPartA();
            this._builder.BuildPartB();
            this._builder.BuildPartC();
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tạo Director
            var director = new Director();

            // Tạo Builder
            var builder = new ConcreteBuilder();

            // Gán Builder cho Director
            director.Builder = builder;

            // Sản phẩm cơ bản
            Console.WriteLine("Standard basic product:");
            director.BuildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            // Sản phẩm đầy đủ
            Console.WriteLine("Standard full featured product:");
            director.BuildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            // Sản phẩm tùy chỉnh
            Console.WriteLine("Custom product:");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.ReadKey();
        }
    }
}