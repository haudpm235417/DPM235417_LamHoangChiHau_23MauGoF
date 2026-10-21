using System;

namespace DPM235417_LamHoangChiHau_BridgePattern_NhanVienBanHang
{
    // Implementor: phần triển khai thiết bị hiển thị.
    public interface IDisplayDevice
    {
        void TurnOn();
        void SetVolume(int volume);
    }

    public class Television : IDisplayDevice
    {
        public void TurnOn() { Console.WriteLine("Tivi da bat."); }
        public void SetVolume(int volume) { Console.WriteLine("Am luong tivi: " + volume); }
    }

    public class StoreSpeaker : IDisplayDevice
    {
        public void TurnOn() { Console.WriteLine("Loa cua hang da bat."); }
        public void SetVolume(int volume) { Console.WriteLine("Am luong loa cua hang: " + volume); }
    }

    // Abstraction giữ tham chiếu đến Implementor; hai chiều có thể thay đổi độc lập.
    public class RemoteControl
    {
        protected readonly IDisplayDevice device;
        public RemoteControl(IDisplayDevice device) { this.device = device; }
        public virtual void PowerOn() { device.TurnOn(); }
        public virtual void SetVolume(int volume) { device.SetVolume(volume); }
    }

    // RefinedAbstraction bổ sung chức năng mà không phải tạo class cho từng thiết bị.
    public class SalesRemoteControl : RemoteControl
    {
        public SalesRemoteControl(IDisplayDevice device) : base(device) { }
        public void PlayPromotion()
        {
            device.TurnOn();
            device.SetVolume(15);
            Console.WriteLine("Dang phat thong diep khuyen mai cua cua hang.");
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: BRIDGE PATTERN ===");

            RemoteControl tvRemote = new RemoteControl(new Television());
            tvRemote.PowerOn();
            tvRemote.SetVolume(20);
            Console.WriteLine();
            SalesRemoteControl speakerRemote = new SalesRemoteControl(new StoreSpeaker());
            speakerRemote.PlayPromotion();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
