
using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Builder
    public interface IBuilder
    {
        void BuildThuocBaoVeThucVat();
        void BuildPhanBon();
        void BuildHuongDanSuDung();
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

        // Thêm thuốc bảo vệ thực vật
        public void BuildThuocBaoVeThucVat()
        {
            this._product.Add("Thuốc bảo vệ thực vật");
        }

        // Thêm phân bón
        public void BuildPhanBon()
        {
            this._product.Add("Phân bón NPK");
        }

        // Thêm hướng dẫn sử dụng
        public void BuildHuongDanSuDung()
        {
            this._product.Add("Hướng dẫn sử dụng");
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
                return "Gói sản phẩm: Không có thành phần.";
            }

            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            // Xóa dấu ", " cuối cùng
            str = str.Remove(str.Length - 2);

            return "Gói sản phẩm: " + str;
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

        // Gói sản phẩm cơ bản
        public void BuildGoiCoBan()
        {
            this._builder.BuildThuocBaoVeThucVat();
        }

        // Gói sản phẩm đầy đủ
        public void BuildGoiDayDu()
        {
            this._builder.BuildThuocBaoVeThucVat();
            this._builder.BuildPhanBon();
            this._builder.BuildHuongDanSuDung();
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

            // Gói sản phẩm cơ bản
            Console.WriteLine("===== GÓI SẢN PHẨM CƠ BẢN =====");
            director.BuildGoiCoBan();
            Console.WriteLine(builder.GetProduct().ListParts());

            // Gói sản phẩm đầy đủ
            Console.WriteLine();
            Console.WriteLine("===== GÓI SẢN PHẨM ĐẦY ĐỦ =====");
            director.BuildGoiDayDu();
            Console.WriteLine(builder.GetProduct().ListParts());

            // Gói sản phẩm tùy chỉnh
            Console.WriteLine();
            Console.WriteLine("===== GÓI SẢN PHẨM TÙY CHỈNH =====");

            builder.BuildThuocBaoVeThucVat();
            builder.BuildHuongDanSuDung();

            Console.WriteLine(builder.GetProduct().ListParts());

            Console.ReadKey();
        }
    }
}
