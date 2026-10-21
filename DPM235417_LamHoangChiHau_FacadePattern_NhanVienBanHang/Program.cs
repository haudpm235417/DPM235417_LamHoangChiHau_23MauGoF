using System;

namespace DPM235417_LamHoangChiHau_FacadePattern_NhanVienBanHang
{
    // Các subsystem độc lập thực hiện từng phần của quy trình thanh toán.
    public class InventoryService
    {
        public bool HasStock(string sku, int quantity)
        {
            Console.WriteLine("Kiem tra ton kho: " + sku + ", so luong " + quantity);
            return quantity > 0 && sku == "PHANBON01";
        }
        public void Reserve(string sku, int quantity) { Console.WriteLine("Da giu " + quantity + " san pham " + sku + "."); }
    }

    public class PaymentService
    {
        public bool Charge(decimal amount) { Console.WriteLine("Thanh toan: " + amount.ToString("N0") + " VND"); return amount > 0; }
    }

    public class ShippingService
    {
        public void ArrangeDelivery(string address) { Console.WriteLine("Tao yeu cau giao den: " + address); }
    }

    // Facade cung cấp một thao tác đơn giản thay cho việc gọi nhiều subsystem.
    public class CheckoutFacade
    {
        private readonly InventoryService inventory = new InventoryService();
        private readonly PaymentService payment = new PaymentService();
        private readonly ShippingService shipping = new ShippingService();

        public bool PlaceOrder(string sku, int quantity, decimal total, string address)
        {
            Console.WriteLine("--- Bat dau xu ly don hang ---");
            if (!inventory.HasStock(sku, quantity)) { Console.WriteLine("Dat hang that bai: khong du hang."); return false; }
            if (!payment.Charge(total)) { Console.WriteLine("Dat hang that bai: thanh toan loi."); return false; }
            inventory.Reserve(sku, quantity);
            shipping.ArrangeDelivery(address);
            Console.WriteLine("Don hang da duoc tiep nhan.");
            return true;
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: FACADE PATTERN ===");

            CheckoutFacade checkout = new CheckoutFacade();
            checkout.PlaceOrder("PHANBON01", 2, 360000m, "12 Nguyen Trai, Tay Ninh");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
