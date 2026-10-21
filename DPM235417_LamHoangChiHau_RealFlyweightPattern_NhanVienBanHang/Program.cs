using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealFlyweightPattern_NhanVienBanHang
{
    // Flyweight chua trang thai noi tai co the chia se.
    public class TreeType
    {
        private readonly string name; private readonly string color; private readonly string texture;
        public TreeType(string name, string color, string texture) { this.name = name; this.color = color; this.texture = texture; }
        public void Draw(int x, int y) { Console.WriteLine("Draw " + name + " tree at (" + x + "," + y + ") color=" + color + " texture=" + texture); }
    }
    public class TreeFactory
    {
        private readonly Dictionary<string, TreeType> treeTypes = new Dictionary<string, TreeType>();
        public TreeType GetTreeType(string name, string color, string texture)
        {
            string key = name + "|" + color + "|" + texture; TreeType type;
            if (!treeTypes.TryGetValue(key, out type)) { type = new TreeType(name, color, texture); treeTypes.Add(key, type); }
            return type;
        }
        public int SharedTypes { get { return treeTypes.Count; } }
    }
    // Tree luu trang thai ngoai rieng (toa do) va dung TreeType duoc chia se.
    public class Tree { private readonly int x; private readonly int y; private readonly TreeType type;
        public Tree(int x, int y, TreeType type) { this.x = x; this.y = y; this.type = type; }
        public void Draw() { type.Draw(x, y); } }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALFLYWEIGHT PATTERN ===");

            TreeFactory factory = new TreeFactory(); List<Tree> forest = new List<Tree>();
            forest.Add(new Tree(1, 5, factory.GetTreeType("Oak", "green", "oak-texture")));
            forest.Add(new Tree(7, 9, factory.GetTreeType("Oak", "green", "oak-texture")));
            forest.Add(new Tree(4, 2, factory.GetTreeType("Pine", "dark-green", "pine-texture")));
            foreach (Tree tree in forest) tree.Draw();
            Console.WriteLine("TreeType objects shared: " + factory.SharedTypes + " for " + forest.Count + " trees.");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
