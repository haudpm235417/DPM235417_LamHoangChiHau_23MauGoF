using System;

namespace DPM235417_LamHoangChiHau_DecoratorPattern_NhanVienBanHang
{
    public interface IProductOffer
    {
        string GetDescription();
        decimal GetPrice();
    }

    public class ProductOffer : IProductOffer
    {
        private readonly string name;
        private readonly decimal price;
        public ProductOffer(string name, decimal price) { this.name = name; this.price = price; }
        public string GetDescription() { return name; }
        public decimal GetPrice() { return price; }
    }

    // Decorator giữ một IProductOffer và bổ sung hành vi cho đối tượng được bọc.
    public abstract class ProductDecorator : IProductOffer
    {
        protected readonly IProductOffer inner;
        protected ProductDecorator(IProductOffer inner) { this.inner = inner; }
        public abstract string GetDescription();
        public abstract decimal GetPrice();
    }

    public class GiftWrapDecorator : ProductDecorator
    {
        private const decimal Fee = 10000m;
        public GiftWrapDecorator(IProductOffer inner) : base(inner) { }
        public override string GetDescription() { return inner.GetDescription() + " + goi qua"; }
        public override decimal GetPrice() { return inner.GetPrice() + Fee; }
    }

    public class SeasonalDiscountDecorator : ProductDecorator
    {
        private readonly decimal rate;
        public SeasonalDiscountDecorator(IProductOffer inner, decimal rate) : base(inner) { this.rate = rate; }
        public override string GetDescription() { return inner.GetDescription() + " + giam gia mua vu"; }
        public override decimal GetPrice() { return inner.GetPrice() * (1m - rate); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: DECORATOR PATTERN ===");

            IProductOffer offer = new ProductOffer("Bo dung cu lam vuon", 250000m);
            Console.WriteLine(offer.GetDescription() + ": " + offer.GetPrice().ToString("N0") + " VND");
            offer = new GiftWrapDecorator(offer);
            offer = new SeasonalDiscountDecorator(offer, 0.10m);
            Console.WriteLine(offer.GetDescription() + ": " + offer.GetPrice().ToString("N0") + " VND");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
