using System;

namespace DPM235417_LamHoangChiHau_TemplateMethodPattern_NhanVienBanHang
{
    // Template Method cố định trình tự tạo báo cáo, còn các bước cụ thể do lớp con định nghĩa.
    public abstract class SalesReport
    {
        public void Generate()
        {
            PrintHeader();
            PrintRows();
            PrintFooter();
        }
        protected virtual void PrintHeader() { Console.WriteLine("=== BAO CAO CUA HANG ==="); }
        protected abstract void PrintRows();
        protected virtual void PrintFooter() { Console.WriteLine("=== HET BAO CAO ==="); }
    }

    public class DailySalesReport : SalesReport
    {
        protected override void PrintRows()
        {
            Console.WriteLine("Ngay: 21/10/2026");
            Console.WriteLine("Doanh thu phan bon: 2.500.000 VND");
            Console.WriteLine("So don hang: 18");
        }
    }
    public class InventoryReport : SalesReport
    {
        protected override void PrintRows()
        {
            Console.WriteLine("Phan NPK: 25 bao");
            Console.WriteLine("Thuoc bao ve thuc vat: 8 chai");
            Console.WriteLine("Can kiem tra cac mat hang ton thap.");
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: TEMPLATEMETHOD PATTERN ===");

            Console.WriteLine("Bao cao doanh thu:");
            new DailySalesReport().Generate();
            Console.WriteLine();
            Console.WriteLine("Bao cao ton kho:");
            new InventoryReport().Generate();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
