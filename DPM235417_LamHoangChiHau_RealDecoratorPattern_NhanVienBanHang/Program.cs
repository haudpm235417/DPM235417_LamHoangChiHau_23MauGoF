using System;

namespace DPM235417_LamHoangChiHau_RealDecoratorPattern_NhanVienBanHang
{
    public interface IDataSource { void WriteData(string data); string ReadData(); }
    public class FileDataSource : IDataSource
    {
        private string content = String.Empty;
        public void WriteData(string data) { content = data; Console.WriteLine("Ghi du lieu goc."); }
        public string ReadData() { return content; }
    }
    public abstract class DataSourceDecorator : IDataSource
    {
        protected readonly IDataSource wrappee;
        protected DataSourceDecorator(IDataSource source) { wrappee = source; }
        public virtual void WriteData(string data) { wrappee.WriteData(data); }
        public virtual string ReadData() { return wrappee.ReadData(); }
    }
    public class EncryptionDecorator : DataSourceDecorator
    {
        public EncryptionDecorator(IDataSource source) : base(source) { }
        public override void WriteData(string data) { wrappee.WriteData("ENC(" + data + ")"); }
        public override string ReadData() { string value = wrappee.ReadData(); return value.StartsWith("ENC(") ? value.Substring(4, value.Length - 5) : value; }
    }
    public class CompressionDecorator : DataSourceDecorator
    {
        public CompressionDecorator(IDataSource source) : base(source) { }
        public override void WriteData(string data) { wrappee.WriteData("ZIP(" + data + ")"); }
        public override string ReadData() { string value = wrappee.ReadData(); return value.StartsWith("ZIP(") ? value.Substring(4, value.Length - 5) : value; }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALDECORATOR PATTERN ===");

            IDataSource source = new FileDataSource();
            IDataSource decorated = new CompressionDecorator(new EncryptionDecorator(source));
            decorated.WriteData("salary=5000");
            Console.WriteLine("Du lieu sau khi doc qua cac decorator: " + decorated.ReadData());
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
