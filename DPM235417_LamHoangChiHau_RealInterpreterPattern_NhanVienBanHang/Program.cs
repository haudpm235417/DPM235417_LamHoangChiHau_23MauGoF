using System;

namespace DPM235417_LamHoangChiHau_RealInterpreterPattern_NhanVienBanHang
{
    // Cac doi tuong bieu dien mot van pham so hoc don gian.
    public interface IExpression { int Interpret(); }
    public class NumberExpression : IExpression
    { private readonly int value; public NumberExpression(int value) { this.value = value; } public int Interpret() { return value; } }
    public class AddExpression : IExpression
    {
        private readonly IExpression left; private readonly IExpression right;
        public AddExpression(IExpression left, IExpression right) { this.left = left; this.right = right; }
        public int Interpret() { return left.Interpret() + right.Interpret(); }
    }
    public class MultiplyExpression : IExpression
    {
        private readonly IExpression left; private readonly IExpression right;
        public MultiplyExpression(IExpression left, IExpression right) { this.left = left; this.right = right; }
        public int Interpret() { return left.Interpret() * right.Interpret(); }
    }
    public class SubtractExpression : IExpression
    {
        private readonly IExpression left; private readonly IExpression right;
        public SubtractExpression(IExpression left, IExpression right) { this.left = left; this.right = right; }
        public int Interpret() { return left.Interpret() - right.Interpret(); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALINTERPRETER PATTERN ===");

            // Bieu thuc: (10 + 2) * (8 - 3) = 60.
            IExpression expression = new MultiplyExpression(
                new AddExpression(new NumberExpression(10), new NumberExpression(2)),
                new SubtractExpression(new NumberExpression(8), new NumberExpression(3)));
            Console.WriteLine("Expression result: " + expression.Interpret());
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
