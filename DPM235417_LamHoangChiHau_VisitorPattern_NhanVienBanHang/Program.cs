using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_VisitorPattern_NhanVienBanHang
{
    // Element xác nhận thao tác với visitor phù hợp với kiểu cụ thể của nó.
    public interface IInventoryItem { void Accept(IInventoryVisitor visitor); }
    public interface IInventoryVisitor
    {
        void Visit(ProductItem product);
        void Visit(ServiceItem service);
    }

    public class ProductItem : IInventoryItem
    {
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public ProductItem(string name, decimal unitPrice, int quantity)
        { Name = name; UnitPrice = unitPrice; Quantity = quantity; }
        public void Accept(IInventoryVisitor visitor) { visitor.Visit(this); }
    }
    public class ServiceItem : IInventoryItem
    {
        public string Name { get; private set; }
        public decimal HourlyRate { get; private set; }
        public int Hours { get; private set; }
        public ServiceItem(string name, decimal hourlyRate, int hours)
        { Name = name; HourlyRate = hourlyRate; Hours = hours; }
        public void Accept(IInventoryVisitor visitor) { visitor.Visit(this); }
    }

    // Visitor bổ sung phép tính định giá mà không sửa các lớp item.
    public class InventoryValuationVisitor : IInventoryVisitor
    {
        public decimal Total { get; private set; }
        public void Visit(ProductItem product)
        {
            decimal value = product.UnitPrice * product.Quantity;
            Total += value;
            Console.WriteLine("Hang hoa " + product.Name + ": " + value.ToString("N0") + " VND");
        }
        public void Visit(ServiceItem service)
        {
            decimal value = service.HourlyRate * service.Hours;
            Total += value;
            Console.WriteLine("Dich vu " + service.Name + ": " + value.ToString("N0") + " VND");
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: VISITOR PATTERN ===");

            List<IInventoryItem> items = new List<IInventoryItem>
            {
                new ProductItem("Phan NPK", 180000m, 10),
                new ProductItem("Binh tuoi cay", 75000m, 4),
                new ServiceItem("Tu van ky thuat", 150000m, 2)
            };
            InventoryValuationVisitor visitor = new InventoryValuationVisitor();
            foreach (IInventoryItem item in items) item.Accept(visitor);
            Console.WriteLine("Tong gia tri dinh gia: " + visitor.Total.ToString("N0") + " VND");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
