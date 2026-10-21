using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealCompositePattern_NhanVienBanHang
{
    public interface IGraphic { void Draw(); }
    public class Dot : IGraphic
    {
        protected int x; protected int y;
        public Dot(int x, int y) { this.x = x; this.y = y; }
        public virtual void Draw() { Console.WriteLine("Draw Dot at (" + x + ", " + y + ")"); }
    }
    public class Circle : Dot
    {
        private readonly int radius;
        public Circle(int x, int y, int radius) : base(x, y) { this.radius = radius; }
        public override void Draw() { Console.WriteLine("Draw Circle at (" + x + ", " + y + ") radius " + radius); }
    }
    // Composite co the chua ca leaf va composite khac.
    public class CompoundGraphic : IGraphic
    {
        private readonly List<IGraphic> children = new List<IGraphic>();
        public void Add(IGraphic graphic) { children.Add(graphic); }
        public void Remove(IGraphic graphic) { children.Remove(graphic); }
        public void Draw() { Console.WriteLine("Draw group:"); foreach (IGraphic graphic in children) graphic.Draw(); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALCOMPOSITE PATTERN ===");

            CompoundGraphic canvas = new CompoundGraphic();
            canvas.Add(new Dot(1, 2)); canvas.Add(new Circle(5, 7, 3));
            CompoundGraphic nested = new CompoundGraphic(); nested.Add(new Dot(9, 9)); nested.Add(new Circle(2, 3, 1));
            canvas.Add(nested); canvas.Draw();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
