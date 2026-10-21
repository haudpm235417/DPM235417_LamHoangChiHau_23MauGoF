using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_CompositePattern_NhanVienBanHang
{
    // Component: leaf và composite cùng thực hiện giao diện này.
    public interface IProductComponent
    {
        string Name { get; }
        decimal GetPrice();
        void Display(string indent);
    }

    // Leaf: một sản phẩm riêng lẻ.
    public class ProductItem : IProductComponent
    {
        public string Name { get; private set; }
        private readonly decimal price;
        public ProductItem(string name, decimal price) { Name = name; this.price = price; }
        public decimal GetPrice() { return price; }
        public void Display(string indent) { Console.WriteLine(indent + "- " + Name + ": " + price.ToString("N0") + " VND"); }
    }

    // Composite: nhóm có thể chứa cả sản phẩm và nhóm khác.
    public class ProductCategory : IProductComponent
    {
        private readonly List<IProductComponent> children = new List<IProductComponent>();
        public string Name { get; private set; }
        public ProductCategory(string name) { Name = name; }
        public void Add(IProductComponent component) { children.Add(component); }
        public decimal GetPrice()
        {
            decimal total = 0;
            foreach (IProductComponent child in children) total += child.GetPrice();
            return total;
        }
        public void Display(string indent)
        {
            Console.WriteLine(indent + "+ Nhom: " + Name);
            foreach (IProductComponent child in children) child.Display(indent + "  ");
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: COMPOSITE PATTERN ===");

            ProductCategory store = new ProductCategory("Cua hang nong nghiep");
            ProductCategory fertilizer = new ProductCategory("Phan bon");
            fertilizer.Add(new ProductItem("Phan NPK 5kg", 180000m));
            fertilizer.Add(new ProductItem("Phan huu co 2kg", 95000m));
            store.Add(fertilizer);
            store.Add(new ProductItem("Binh tuoi cay", 75000m));
            store.Display("");
            Console.WriteLine("Tong gia tri danh muc: " + store.GetPrice().ToString("N0") + " VND");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
