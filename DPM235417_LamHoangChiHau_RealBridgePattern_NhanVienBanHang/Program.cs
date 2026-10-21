using System;

namespace DPM235417_LamHoangChiHau_RealBridgePattern_NhanVienBanHang
{
    // Implementor: thiet bi co the duoc dieu khien theo cung hop dong.
    public interface IDevice { bool IsEnabled(); void Enable(); void Disable(); int GetVolume(); void SetVolume(int percent); }
    public class TV : IDevice
    {
        private bool enabled; private int volume = 20;
        public bool IsEnabled() { return enabled; }
        public void Enable() { enabled = true; Console.WriteLine("TV: bat."); }
        public void Disable() { enabled = false; Console.WriteLine("TV: tat."); }
        public int GetVolume() { return volume; }
        public void SetVolume(int percent) { volume = percent; Console.WriteLine("TV volume=" + volume); }
    }
    public class Radio : IDevice
    {
        private bool enabled; private int volume = 10;
        public bool IsEnabled() { return enabled; }
        public void Enable() { enabled = true; Console.WriteLine("Radio: bat."); }
        public void Disable() { enabled = false; Console.WriteLine("Radio: tat."); }
        public int GetVolume() { return volume; }
        public void SetVolume(int percent) { volume = percent; Console.WriteLine("Radio volume=" + volume); }
    }
    // Abstraction giu tham chieu den IDevice; remote va thiet bi thay doi doc lap.
    public class RemoteControl
    {
        protected readonly IDevice device;
        public RemoteControl(IDevice device) { this.device = device; }
        public void TogglePower() { if (device.IsEnabled()) device.Disable(); else device.Enable(); }
        public void VolumeUp() { device.SetVolume(Math.Min(100, device.GetVolume() + 10)); }
    }
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device) { }
        public void Mute() { device.SetVolume(0); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALBRIDGE PATTERN ===");

            RemoteControl remote = new RemoteControl(new TV()); remote.TogglePower(); remote.VolumeUp();
            AdvancedRemoteControl advanced = new AdvancedRemoteControl(new Radio()); advanced.TogglePower(); advanced.Mute();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
