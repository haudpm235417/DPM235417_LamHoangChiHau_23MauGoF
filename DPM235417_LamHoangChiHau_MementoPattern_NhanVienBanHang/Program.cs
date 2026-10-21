using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_MementoPattern_NhanVienBanHang
{
    // Memento lưu trạng thái mà không để History sửa trực tiếp nội dung bên trong.
    public sealed class ProductMemento
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductMemento(string name, decimal price) { Name = name; Price = price; }
    }

    // Originator tạo và khôi phục memento.
    public class ProductEditor
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public void Edit(string name, decimal price) { Name = name; Price = price; }
        public ProductMemento Save() { return new ProductMemento(Name, Price); }
        public void Restore(ProductMemento memento) { Name = memento.Name; Price = memento.Price; }
        public void Display() { Console.WriteLine(Name + " - " + Price.ToString("N0") + " VND"); }
    }

    // Caretaker chỉ quản lý lịch sử snapshot, không phân tích nội dung snapshot.
    public class EditHistory
    {
        private readonly Stack<ProductMemento> history = new Stack<ProductMemento>();
        public void Save(ProductMemento state) { history.Push(state); }
        public ProductMemento Undo() { return history.Count > 0 ? history.Pop() : null; }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: MEMENTO PATTERN ===");

            ProductEditor editor = new ProductEditor();
            EditHistory history = new EditHistory();
            editor.Edit("Phan NPK", 180000m);
            history.Save(editor.Save());
            editor.Edit("Phan NPK - gia moi", 210000m);
            Console.WriteLine("Sau khi sua:"); editor.Display();
            ProductMemento previous = history.Undo();
            if (previous != null) editor.Restore(previous);
            Console.WriteLine("Sau khi Undo:"); editor.Display();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
