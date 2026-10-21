using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_ObserverPattern_NhanVienBanHang
{
    public interface IStockObserver { void Update(string productName, int quantity); }
    public interface IStockSubject
    {
        void Subscribe(IStockObserver observer);
        void Unsubscribe(IStockObserver observer);
        void SetQuantity(string productName, int quantity);
    }

    // Subject thông báo các observer khi tồn kho thay đổi.
    public class StockMonitor : IStockSubject
    {
        private readonly List<IStockObserver> observers = new List<IStockObserver>();
        public void Subscribe(IStockObserver observer) { if (!observers.Contains(observer)) observers.Add(observer); }
        public void Unsubscribe(IStockObserver observer) { observers.Remove(observer); }
        public void SetQuantity(string productName, int quantity)
        {
            Console.WriteLine("Cap nhat ton kho: " + productName + " = " + quantity);
            foreach (IStockObserver observer in observers) observer.Update(productName, quantity);
        }
    }

    public class StoreManagerObserver : IStockObserver
    {
        public void Update(string productName, int quantity)
        {
            if (quantity < 10) Console.WriteLine("[Quan ly] Can dat them " + productName + ".");
            else Console.WriteLine("[Quan ly] Ton kho " + productName + " dang du.");
        }
    }
    public class SalesStaffObserver : IStockObserver
    {
        public void Update(string productName, int quantity)
        { Console.WriteLine("[Nhan vien] Da nhan thong bao ton kho moi cua " + productName + "."); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: OBSERVER PATTERN ===");

            StockMonitor monitor = new StockMonitor();
            monitor.Subscribe(new StoreManagerObserver());
            monitor.Subscribe(new SalesStaffObserver());
            monitor.SetQuantity("Phan NPK", 25);
            Console.WriteLine();
            monitor.SetQuantity("Thuoc tru sau", 4);
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
