using System;

namespace DPM235417_LamHoangChiHau_InterpreterPattern_NhanVienBanHang
{
    // AbstractExpression: mọi nút trong cây biểu thức đều có thể tự diễn giải.
    public interface IExpression { decimal Interpret(); }

    public class NumberExpression : IExpression
    {
        private readonly decimal value;
        public NumberExpression(decimal value) { this.value = value; }
        public decimal Interpret() { return value; }
    }

    public class AddExpression : IExpression
    {
        private readonly IExpression left; private readonly IExpression right;
        public AddExpression(IExpression left, IExpression right) { this.left = left; this.right = right; }
        public decimal Interpret() { return left.Interpret() + right.Interpret(); }
    }

    public class MultiplyExpression : IExpression
    {
        private readonly IExpression left; private readonly IExpression right;
        public MultiplyExpression(IExpression left, IExpression right) { this.left = left; this.right = right; }
        public decimal Interpret() { return left.Interpret() * right.Interpret(); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: INTERPRETER PATTERN ===");

            // Biểu thức tương đương (120000 + 80000) * 0.9: áp dụng giảm giá 10%.
            IExpression salePrice = new MultiplyExpression(
                new AddExpression(new NumberExpression(120000m), new NumberExpression(80000m)),
                new NumberExpression(0.9m));
            Console.WriteLine("Gia sau khi giam 10%: " + salePrice.Interpret().ToString("N0") + " VND");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
