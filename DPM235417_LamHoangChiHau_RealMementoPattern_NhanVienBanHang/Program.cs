using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealMementoPattern_NhanVienBanHang
{
    public sealed class EditorSnapshot
    {
        public string Text { get; private set; }
        public EditorSnapshot(string text) { Text = text; }
    }
    // Originator tao snapshot va phuc hoi trang thai tu snapshot.
    public class TextEditor
    {
        public string Text { get; set; }
        public TextEditor(string text) { Text = text; }
        public EditorSnapshot Save() { return new EditorSnapshot(Text); }
        public void Restore(EditorSnapshot snapshot) { Text = snapshot.Text; }
        public void Print() { Console.WriteLine("Editor text: " + Text); }
    }
    // Caretaker giu cac trang thai da luu, khong sua noi dung ben trong snapshot.
    public class History
    {
        private readonly Stack<EditorSnapshot> snapshots = new Stack<EditorSnapshot>();
        public void Backup(TextEditor editor) { snapshots.Push(editor.Save()); }
        public void Undo(TextEditor editor) { if (snapshots.Count > 0) editor.Restore(snapshots.Pop()); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALMEMENTO PATTERN ===");

            TextEditor editor = new TextEditor("Hello"); History history = new History();
            history.Backup(editor); editor.Text = "Hello, world"; editor.Print();
            history.Backup(editor); editor.Text = "Unsaved change"; editor.Print();
            history.Undo(editor); editor.Print(); history.Undo(editor); editor.Print();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
