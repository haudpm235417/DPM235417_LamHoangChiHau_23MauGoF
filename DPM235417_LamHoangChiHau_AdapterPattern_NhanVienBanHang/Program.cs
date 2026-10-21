using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_AdapterPattern_NhanVienBanHang
{
    // Target: giao diện mà hệ thống bán hàng đang sử dụng.
    public interface IStockService
    {
        int GetAvailableQuantity(string productCode);
    }

    // Adaptee: API kho cũ có tên phương thức khác với giao diện mong muốn.
    public class LegacyWarehouseApi
    {
        private readonly Dictionary<string, int> quantities = new Dictionary<string, int>
        {
            { "PHANBON01", 35 }, { "THUOC01", 12 }
        };

        public int QueryQuantityBySku(string sku)
        {
            int quantity;
            return quantities.TryGetValue(sku, out quantity) ? quantity : 0;
        }
    }

    // Adapter chuyển lời gọi từ IStockService sang API kho cũ.
    public class LegacyWarehouseAdapter : IStockService
    {
        private readonly LegacyWarehouseApi warehouse;
        public LegacyWarehouseAdapter(LegacyWarehouseApi warehouse) { this.warehouse = warehouse; }
        public int GetAvailableQuantity(string productCode) { return warehouse.QueryQuantityBySku(productCode); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: ADAPTER PATTERN ===");

            IStockService stock = new LegacyWarehouseAdapter(new LegacyWarehouseApi());
            Console.WriteLine("So luong PHANBON01 trong kho: " + stock.GetAvailableQuantity("PHANBON01"));
            Console.WriteLine("So luong MAKHONGTONTAI trong kho: " + stock.GetAvailableQuantity("MAKHONGTONTAI"));
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
