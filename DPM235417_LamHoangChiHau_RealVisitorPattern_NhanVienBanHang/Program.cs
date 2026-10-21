using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealVisitorPattern_NhanVienBanHang
{
    public interface IShape { void Accept(IShapeVisitor visitor); }
    public class Dot : IShape
    {
        public int X { get; private set; } public int Y { get; private set; }
        public Dot(int x, int y) { X = x; Y = y; }
        public virtual void Accept(IShapeVisitor visitor) { visitor.VisitDot(this); }
    }
    public class Circle : Dot
    {
        public int Radius { get; private set; }
        public Circle(int x, int y, int radius) : base(x, y) { Radius = radius; }
        public override void Accept(IShapeVisitor visitor) { visitor.VisitCircle(this); }
    }
    public class Rectangle : IShape
    {
        public int X { get; private set; } public int Y { get; private set; } public int Width { get; private set; } public int Height { get; private set; }
        public Rectangle(int x, int y, int width, int height) { X = x; Y = y; Width = width; Height = height; }
        public void Accept(IShapeVisitor visitor) { visitor.VisitRectangle(this); }
    }
    public interface IShapeVisitor { void VisitDot(Dot dot); void VisitCircle(Circle circle); void VisitRectangle(Rectangle rectangle); }
    public class XmlExportVisitor : IShapeVisitor
    {
        public void VisitDot(Dot dot) { Console.WriteLine("<dot x=\"" + dot.X + "\" y=\"" + dot.Y + "\" />"); }
        public void VisitCircle(Circle circle) { Console.WriteLine("<circle x=\"" + circle.X + "\" y=\"" + circle.Y + "\" radius=\"" + circle.Radius + "\" />"); }
        public void VisitRectangle(Rectangle r) { Console.WriteLine("<rectangle x=\"" + r.X + "\" y=\"" + r.Y + "\" width=\"" + r.Width + "\" height=\"" + r.Height + "\" />"); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALVISITOR PATTERN ===");

            List<IShape> shapes = new List<IShape>(); shapes.Add(new Dot(1, 2)); shapes.Add(new Circle(5, 3, 8)); shapes.Add(new Rectangle(0, 0, 10, 20));
            XmlExportVisitor visitor = new XmlExportVisitor(); foreach (IShape shape in shapes) shape.Accept(visitor);
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
