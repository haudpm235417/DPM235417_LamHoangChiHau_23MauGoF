using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_CommandPattern_NhanVienBanHang
{
    // Receiver: đối tượng thực hiện thao tác tồn kho.
    public class Inventory
    {
        private readonly Dictionary<string, int> stock = new Dictionary<string, int> { { "PHANBON01", 10 } };
        public void ChangeQuantity(string sku, int delta)
        {
            int current;
            if (!stock.TryGetValue(sku, out current)) current = 0;
            if (current + delta < 0) throw new InvalidOperationException("Ton kho khong du.");
            stock[sku] = current + delta;
            Console.WriteLine("Ton kho " + sku + " = " + stock[sku]);
        }
    }

    public interface ICommand { void Execute(); }
    public class AddStockCommand : ICommand
    {
        private readonly Inventory inventory; private readonly string sku; private readonly int quantity;
        public AddStockCommand(Inventory inventory, string sku, int quantity)
        { this.inventory = inventory; this.sku = sku; this.quantity = quantity; }
        public void Execute() { inventory.ChangeQuantity(sku, quantity); }
    }
    public class RemoveStockCommand : ICommand
    {
        private readonly Inventory inventory; private readonly string sku; private readonly int quantity;
        public RemoveStockCommand(Inventory inventory, string sku, int quantity)
        { this.inventory = inventory; this.sku = sku; this.quantity = quantity; }
        public void Execute() { inventory.ChangeQuantity(sku, -quantity); }
    }

    // Invoker nhận command và kích hoạt mà không cần biết chi tiết nghiệp vụ.
    public class InventoryOperator
    {
        public void Run(ICommand command) { command.Execute(); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: COMMAND PATTERN ===");

            Inventory inventory = new Inventory();
            InventoryOperator operatorTool = new InventoryOperator();
            operatorTool.Run(new AddStockCommand(inventory, "PHANBON01", 5));
            operatorTool.Run(new RemoveStockCommand(inventory, "PHANBON01", 3));
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
