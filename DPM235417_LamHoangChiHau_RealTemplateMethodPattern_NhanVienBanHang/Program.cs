using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealTemplateMethodPattern_NhanVienBanHang
{
    // Template Method co dinh cac buoc xu ly, cac lop con dinh nghia thao tac cu the.
    public abstract class DataMiner
    {
        public void Mine(string path)
        {
            string file = OpenFile(path); string raw = ExtractData(file);
            List<string> records = ParseData(raw); AnalyzeData(records); SendReport(records); CloseFile(file);
        }
        protected abstract string OpenFile(string path);
        protected abstract string ExtractData(string file);
        protected abstract List<string> ParseData(string raw);
        protected virtual void AnalyzeData(List<string> records) { Console.WriteLine("Analyze " + records.Count + " records."); }
        protected virtual void SendReport(List<string> records) { Console.WriteLine("Report generated."); }
        protected virtual void CloseFile(string file) { Console.WriteLine("Close " + file); }
    }
    public class CsvDataMiner : DataMiner
    {
        protected override string OpenFile(string path) { Console.WriteLine("Open CSV: " + path); return path; }
        protected override string ExtractData(string file) { return "a,b,c"; }
        protected override List<string> ParseData(string raw) { Console.WriteLine("Parse CSV rows."); return new List<string>(raw.Split(',')); }
    }
    public class JsonDataMiner : DataMiner
    {
        protected override string OpenFile(string path) { Console.WriteLine("Open JSON: " + path); return path; }
        protected override string ExtractData(string file) { return "id,name"; }
        protected override List<string> ParseData(string raw) { Console.WriteLine("Parse JSON fields."); return new List<string>(raw.Split(',')); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALTEMPLATEMETHOD PATTERN ===");
 new CsvDataMiner().Mine("data.csv"); Console.WriteLine(); new JsonDataMiner().Mine("data.json"); 
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
