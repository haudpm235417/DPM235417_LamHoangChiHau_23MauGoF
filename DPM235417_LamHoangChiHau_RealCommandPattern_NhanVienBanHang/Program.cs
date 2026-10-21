using System;

namespace DPM235417_LamHoangChiHau_RealCommandPattern_NhanVienBanHang
{
    public interface ICommand { void Execute(); }
    public class Light
    {
        public void TurnOn() { Console.WriteLine("Light is ON"); }
        public void TurnOff() { Console.WriteLine("Light is OFF"); }
    }
    public class LightOnCommand : ICommand
    { private readonly Light light; public LightOnCommand(Light light) { this.light = light; } public void Execute() { light.TurnOn(); } }
    public class LightOffCommand : ICommand
    { private readonly Light light; public LightOffCommand(Light light) { this.light = light; } public void Execute() { light.TurnOff(); } }
    // Invoker khong can biet chi tiet receiver thuc hien lenh nhu the nao.
    public class RemoteButton
    {
        private ICommand command;
        public void SetCommand(ICommand command) { this.command = command; }
        public void Press() { if (command == null) Console.WriteLine("No command assigned."); else command.Execute(); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALCOMMAND PATTERN ===");

            Light light = new Light(); RemoteButton button = new RemoteButton();
            button.SetCommand(new LightOnCommand(light)); button.Press();
            button.SetCommand(new LightOffCommand(light)); button.Press();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
