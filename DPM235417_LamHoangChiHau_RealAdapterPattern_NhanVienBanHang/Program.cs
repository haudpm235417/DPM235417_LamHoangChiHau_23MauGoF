using System;

namespace DPM235417_LamHoangChiHau_RealAdapterPattern_NhanVienBanHang
{
    // Target: giao dien ma ung dung hien tai mong muon.
    public interface IRoundPeg { double GetRadius(); }
    public class RoundHole
    {
        private readonly double radius;
        public RoundHole(double radius) { this.radius = radius; }
        public bool Fits(IRoundPeg peg) { return radius >= peg.GetRadius(); }
    }
    public class RoundPeg : IRoundPeg
    {
        private readonly double radius;
        public RoundPeg(double radius) { this.radius = radius; }
        public double GetRadius() { return radius; }
    }
    // Adaptee co giao dien khong tuong thich voi RoundHole.
    public class SquarePeg
    {
        private readonly double width;
        public SquarePeg(double width) { this.width = width; }
        public double GetWidth() { return width; }
    }
    // Adapter chuyen square peg thanh giao dien round peg.
    public class SquarePegAdapter : IRoundPeg
    {
        private readonly SquarePeg peg;
        public SquarePegAdapter(SquarePeg peg) { this.peg = peg; }
        public double GetRadius() { return Math.Sqrt(peg.GetWidth() * peg.GetWidth() * 2) / 2; }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALADAPTER PATTERN ===");

            RoundHole hole = new RoundHole(5);
            Console.WriteLine("Round peg radius 5 fits: " + hole.Fits(new RoundPeg(5)));
            Console.WriteLine("Square peg width 10 fits: " + hole.Fits(new SquarePegAdapter(new SquarePeg(10))));
            Console.WriteLine("Square peg width 6 fits: " + hole.Fits(new SquarePegAdapter(new SquarePeg(6))));
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
