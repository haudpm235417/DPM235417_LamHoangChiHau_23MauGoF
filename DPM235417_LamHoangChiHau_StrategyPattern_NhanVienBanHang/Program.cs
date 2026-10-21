using System;

namespace DPM235417_LamHoangChiHau_StrategyPattern_NhanVienBanHang
{
    // Strategy định nghĩa một họ thuật toán tính ưu đãi có thể hoán đổi.
    public interface IDiscountStrategy { decimal CalculateDiscount(decimal orderTotal); }
    public class NoDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(decimal orderTotal) { return 0m; }
    }
    public class MemberDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(decimal orderTotal) { return orderTotal * 0.10m; }
    }
    public class SeasonalDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(decimal orderTotal) { return Math.Min(orderTotal * 0.15m, 50000m); }
    }

    // Context sử dụng strategy được cung cấp mà không phụ thuộc lớp cụ thể.
    public class CheckoutCalculator
    {
        private IDiscountStrategy strategy;
        public CheckoutCalculator(IDiscountStrategy strategy) { this.strategy = strategy; }
        public void SetStrategy(IDiscountStrategy strategy) { this.strategy = strategy; }
        public decimal GetFinalPrice(decimal total) { return total - strategy.CalculateDiscount(total); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: STRATEGY PATTERN ===");

            decimal total = 400000m;
            CheckoutCalculator checkout = new CheckoutCalculator(new NoDiscount());
            Console.WriteLine("Khong uu dai: " + checkout.GetFinalPrice(total).ToString("N0") + " VND");
            checkout.SetStrategy(new MemberDiscount());
            Console.WriteLine("Thanh vien: " + checkout.GetFinalPrice(total).ToString("N0") + " VND");
            checkout.SetStrategy(new SeasonalDiscount());
            Console.WriteLine("Khuyen mai mua vu: " + checkout.GetFinalPrice(total).ToString("N0") + " VND");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
