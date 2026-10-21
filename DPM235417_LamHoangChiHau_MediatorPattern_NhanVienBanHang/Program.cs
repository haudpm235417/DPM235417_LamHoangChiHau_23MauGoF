using System;

namespace DPM235417_LamHoangChiHau_MediatorPattern_NhanVienBanHang
{
    // Mediator điều phối tương tác giữa các thành phần giao diện.
    public interface ICheckoutMediator
    {
        void SelectProduct(string name, decimal price);
        void SelectPayment(string method);
        void ConfirmOrder();
    }

    public class ProductSelector
    {
        private readonly ICheckoutMediator mediator;
        public ProductSelector(ICheckoutMediator mediator) { this.mediator = mediator; }
        public void Choose(string name, decimal price) { mediator.SelectProduct(name, price); }
    }
    public class PaymentSelector
    {
        private readonly ICheckoutMediator mediator;
        public PaymentSelector(ICheckoutMediator mediator) { this.mediator = mediator; }
        public void Choose(string method) { mediator.SelectPayment(method); }
    }
    public class ConfirmButton
    {
        private readonly ICheckoutMediator mediator;
        public ConfirmButton(ICheckoutMediator mediator) { this.mediator = mediator; }
        public void Click() { mediator.ConfirmOrder(); }
    }

    public class CheckoutMediator : ICheckoutMediator
    {
        private string productName;
        private decimal price;
        private string paymentMethod;
        public void SelectProduct(string name, decimal price)
        { productName = name; this.price = price; Console.WriteLine("Da chon san pham: " + name); }
        public void SelectPayment(string method)
        { paymentMethod = method; Console.WriteLine("Da chon thanh toan: " + method); }
        public void ConfirmOrder()
        {
            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(paymentMethod))
            { Console.WriteLine("Vui long chon san pham va phuong thuc thanh toan."); return; }
            Console.WriteLine("Xac nhan don: " + productName + " | " + price.ToString("N0") + " VND | " + paymentMethod);
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: MEDIATOR PATTERN ===");

            CheckoutMediator mediator = new CheckoutMediator();
            ProductSelector products = new ProductSelector(mediator);
            PaymentSelector payments = new PaymentSelector(mediator);
            ConfirmButton confirm = new ConfirmButton(mediator);
            products.Choose("Phan huu co 2kg", 95000m);
            payments.Choose("Chuyen khoan");
            confirm.Click();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
