using System;
using System.Collections;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_IteratorPattern_NhanVienBanHang
{
    public class Product
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public Product(string code, string name) { Code = code; Name = name; }
    }

    // Aggregate tạo iterator; người dùng không cần biết cấu trúc lưu trữ bên trong.
    public class ProductCollection : IEnumerable<Product>
    {
        private readonly List<Product> products = new List<Product>();
        public void Add(Product product) { products.Add(product); }
        public IEnumerator<Product> GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++) yield return products[i];
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: ITERATOR PATTERN ===");

            ProductCollection shelf = new ProductCollection();
            shelf.Add(new Product("PB01", "Phan NPK"));
            shelf.Add(new Product("DC02", "Binh tuoi cay"));
            shelf.Add(new Product("TP03", "Hat giong ca chua"));
            Console.WriteLine("Danh sach san pham tren ke:");
            foreach (Product product in shelf)
                Console.WriteLine(product.Code + " - " + product.Name);
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
