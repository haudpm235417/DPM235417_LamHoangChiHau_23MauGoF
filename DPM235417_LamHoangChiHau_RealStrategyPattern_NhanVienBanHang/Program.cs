using System;

namespace DPM235417_LamHoangChiHau_RealStrategyPattern_NhanVienBanHang
{
    public interface IStrategy { int Execute(int a, int b); }
    public class ConcreteStrategyAdd : IStrategy { public int Execute(int a, int b) { return a + b; } }
    public class ConcreteStrategySubtract : IStrategy { public int Execute(int a, int b) { return a - b; } }
    public class ConcreteStrategyMultiply : IStrategy { public int Execute(int a, int b) { return a * b; } }
    // Context su dung chien luoc duoc truyen vao, khong can biet thuat toan cu the.
    public class Context
    {
        private IStrategy strategy;
        public Context(IStrategy strategy) { this.strategy = strategy; }
        public void SetStrategy(IStrategy strategy) { this.strategy = strategy; }
        public int ExecuteStrategy(int a, int b) { return strategy.Execute(a, b); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALSTRATEGY PATTERN ===");

            Context context = new Context(new ConcreteStrategyAdd()); Console.WriteLine("Add: " + context.ExecuteStrategy(10, 5));
            context.SetStrategy(new ConcreteStrategySubtract()); Console.WriteLine("Subtract: " + context.ExecuteStrategy(10, 5));
            context.SetStrategy(new ConcreteStrategyMultiply()); Console.WriteLine("Multiply: " + context.ExecuteStrategy(10, 5));
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
