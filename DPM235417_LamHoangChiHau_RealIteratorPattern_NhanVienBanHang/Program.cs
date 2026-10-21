using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealIteratorPattern_NhanVienBanHang
{
    public interface IWordsIterator { bool HasNext(); string Next(); }
    public class WordsCollection
    {
        private readonly List<string> items = new List<string>();
        public void AddItem(string item) { items.Add(item); }
        public IWordsIterator CreateForwardIterator() { return new ForwardIterator(items); }
        public IWordsIterator CreateReverseIterator() { return new ReverseIterator(items); }
        private class ForwardIterator : IWordsIterator
        {
            private readonly IList<string> items; private int position;
            public ForwardIterator(IList<string> items) { this.items = items; }
            public bool HasNext() { return position < items.Count; }
            public string Next() { return HasNext() ? items[position++] : null; }
        }
        private class ReverseIterator : IWordsIterator
        {
            private readonly IList<string> items; private int position;
            public ReverseIterator(IList<string> items) { this.items = items; position = items.Count - 1; }
            public bool HasNext() { return position >= 0; }
            public string Next() { return HasNext() ? items[position--] : null; }
        }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALITERATOR PATTERN ===");

            WordsCollection words = new WordsCollection(); words.AddItem("First"); words.AddItem("Second"); words.AddItem("Third");
            IWordsIterator iterator = words.CreateForwardIterator(); Console.Write("Forward: "); while (iterator.HasNext()) Console.Write(iterator.Next() + " ");
            iterator = words.CreateReverseIterator(); Console.Write("\nReverse: "); while (iterator.HasNext()) Console.Write(iterator.Next() + " "); Console.WriteLine();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
