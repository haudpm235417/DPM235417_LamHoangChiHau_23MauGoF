using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_ProxyPattern_NhanVienBanHang
{
    public interface IPriceCatalog
    {
        decimal GetPrice(string sku);
    }

    // RealSubject truy cập bảng giá thực tế.
    public class PriceCatalog : IPriceCatalog
    {
        private readonly Dictionary<string, decimal> prices = new Dictionary<string, decimal>
        {
            { "PHANBON01", 180000m }, { "THUOC01", 95000m }
        };
        public decimal GetPrice(string sku)
        {
            decimal price;
            if (!prices.TryGetValue(sku, out price)) throw new ArgumentException("Khong tim thay ma san pham.");
            Console.WriteLine("[Catalog that] Da doc bang gia.");
            return price;
        }
    }

    // Proxy kiểm tra quyền trước khi chuyển lời gọi đến RealSubject.
    public class PriceCatalogProxy : IPriceCatalog
    {
        private readonly IPriceCatalog realCatalog;
        private readonly string role;
        public PriceCatalogProxy(IPriceCatalog realCatalog, string role) { this.realCatalog = realCatalog; this.role = role; }
        public decimal GetPrice(string sku)
        {
            if (role != "NhanVien" && role != "QuanLy")
                throw new UnauthorizedAccessException("Tai khoan khong co quyen xem bang gia noi bo.");
            Console.WriteLine("[Proxy] Da xac thuc quyen cua vai tro " + role + ".");
            return realCatalog.GetPrice(sku);
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: PROXY PATTERN ===");

            IPriceCatalog catalog = new PriceCatalogProxy(new PriceCatalog(), "NhanVien");
            Console.WriteLine("Gia PHANBON01: " + catalog.GetPrice("PHANBON01").ToString("N0") + " VND");
            try
            {
                IPriceCatalog guestCatalog = new PriceCatalogProxy(new PriceCatalog(), "Khach");
                Console.WriteLine(guestCatalog.GetPrice("PHANBON01"));
            }
            catch (UnauthorizedAccessException ex) { Console.WriteLine("Tu choi truy cap: " + ex.Message); }
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
