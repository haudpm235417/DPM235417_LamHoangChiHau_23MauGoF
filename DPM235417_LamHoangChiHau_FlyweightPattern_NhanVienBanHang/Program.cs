using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_FlyweightPattern_NhanVienBanHang
{
    // Flyweight chứa dữ liệu dùng chung giữa nhiều bản ghi sản phẩm.
    public class ProductModel
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Warranty { get; private set; }
        public ProductModel(string name, string category, string warranty)
        { Name = name; Category = category; Warranty = warranty; }
    }

    public class ProductModelFactory
    {
        private readonly Dictionary<string, ProductModel> models = new Dictionary<string, ProductModel>();
        public ProductModel GetModel(string name, string category, string warranty)
        {
            string key = name + "|" + category + "|" + warranty;
            ProductModel model;
            if (!models.TryGetValue(key, out model))
            {
                model = new ProductModel(name, category, warranty);
                models.Add(key, model);
            }
            return model;
        }
        public int ModelCount { get { return models.Count; } }
    }

    // Context chứa dữ liệu riêng của từng điểm bán, nhưng chia sẻ ProductModel.
    public class ProductListing
    {
        private readonly ProductModel model;
        private readonly string storeName;
        private readonly decimal price;
        public ProductListing(ProductModel model, string storeName, decimal price)
        { this.model = model; this.storeName = storeName; this.price = price; }
        public void Display()
        {
            Console.WriteLine(model.Name + " | " + model.Category + " | Bao hanh: " + model.Warranty +
                " | Cua hang: " + storeName + " | Gia: " + price.ToString("N0") + " VND");
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: FLYWEIGHT PATTERN ===");

            ProductModelFactory factory = new ProductModelFactory();
            ProductModel modelA = factory.GetModel("May phun 2L", "Dung cu", "12 thang");
            ProductModel modelB = factory.GetModel("May phun 2L", "Dung cu", "12 thang");
            ProductListing listing1 = new ProductListing(modelA, "Tay Ninh", 320000m);
            ProductListing listing2 = new ProductListing(modelB, "Long An", 315000m);
            listing1.Display();
            listing2.Display();
            Console.WriteLine("Dung chung cung mot doi tuong model: " + Object.ReferenceEquals(modelA, modelB));
            Console.WriteLine("So model duoc tao: " + factory.ModelCount);
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
