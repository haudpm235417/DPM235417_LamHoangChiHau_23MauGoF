
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
    // Factory tạo nhóm sản phẩm thông thường
    class FactorySanPhamThongThuong : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ThuocBaoVeThucVat();
        }

        public IAbstractProductB CreateProductB()
        {
            return new PhanBonNPK();
        }
    }

    // Concrete Factory 2
    // Factory tạo nhóm sản phẩm hữu cơ
    class FactorySanPhamHuuCo : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ThuocSinhHoc();
        }

        public IAbstractProductB CreateProductB()
        {
            return new PhanBonHuuCo();
        }
    }

    // Abstract Product A
    public interface IAbstractProductA
    {
        string UsefulFunctionA();
    }

    // Concrete Product A1
    // Thuốc bảo vệ thực vật
    class ThuocBaoVeThucVat : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "Thuốc bảo vệ thực vật thông thường";
        }
    }

    // Concrete Product A2
    // Thuốc sinh học
    class ThuocSinhHoc : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "Thuốc bảo vệ thực vật sinh học";
        }
    }

    // Abstract Product B
    public interface IAbstractProductB
    {
        string UsefulFunctionB();

        string AnotherUsefulFunctionB(IAbstractProductA collaborator);
    }

    // Concrete Product B1
    // Phân bón NPK
    class PhanBonNPK : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "Phân bón NPK";
        }

        public string AnotherUsefulFunctionB(
            IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return "Phân bón NPK được sử dụng cùng với ("
                + result + ")";
        }
    }

    // Concrete Product B2
    // Phân bón hữu cơ
    class PhanBonHuuCo : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "Phân bón hữu cơ";
        }

        public string AnotherUsefulFunctionB(
            IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return "Phân bón hữu cơ được sử dụng cùng với ("
                + result + ")";
        }
    }

    // Client
    class Client
    {
        public void Main()
        {
            Console.WriteLine(
                "Client: Kiểm tra nhóm sản phẩm thông thường..."
            );

            ClientMethod(new FactorySanPhamThongThuong());
            Console.WriteLine();

            Console.WriteLine(
                "Client: Kiểm tra nhóm sản phẩm hữu cơ..."
            );

            ClientMethod(new FactorySanPhamHuuCo());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            // Tạo Product A và Product B
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            // Sử dụng Product A
            Console.WriteLine(
                "Sản phẩm A: "
                + productA.UsefulFunctionA()
            );

            // Sử dụng Product B
            Console.WriteLine(
                "Sản phẩm B: "
                + productB.UsefulFunctionB()
            );

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
