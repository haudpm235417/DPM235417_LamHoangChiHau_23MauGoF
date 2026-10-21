using System;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Creator
    abstract class Creator
    {
        public abstract IProduct FactoryMethod();

        public string SomeOperation()
        {
            // Gọi Factory Method để tạo sản phẩm
            var product = FactoryMethod();

            // Sử dụng sản phẩm
            var result = "Cửa hàng: Đã tạo sản phẩm - "
                + product.Operation();

            return result;
        }
    }

    // Concrete Creator 1
    class CreatorThuocBaoVeThucVat : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ThuocBaoVeThucVat();
        }
    }

    // Concrete Creator 2
    class CreatorPhanBon : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new PhanBon();
        }
    }

    // Product
    public interface IProduct
    {
        string Operation();
    }

    // Concrete Product 1
    class ThuocBaoVeThucVat : IProduct
    {
        public string Operation()
        {
            return "Thuốc bảo vệ thực vật";
        }
    }

    // Concrete Product 2
    class PhanBon : IProduct
    {
        public string Operation()
        {
            return "Phân bón";
        }
    }

    // Client
    class Client
    {
        public void Main()
        {
            Console.WriteLine(
                "App: Khởi động với Creator tạo thuốc bảo vệ thực vật."
            );

            ClientCode(new CreatorThuocBaoVeThucVat());

            Console.WriteLine();

            Console.WriteLine(
                "App: Khởi động với Creator tạo phân bón."
            );

            ClientCode(new CreatorPhanBon());
        }

        public void ClientCode(Creator creator)
        {
            Console.WriteLine(
                "Client: Không cần biết Creator cụ thể là lớp nào, " +
                "nhưng vẫn có thể tạo và sử dụng sản phẩm.\n" +
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
