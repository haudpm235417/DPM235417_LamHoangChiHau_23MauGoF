using System;

namespace DPM235417_LamHoangChiHau_RealChainOfResponsibilityPattern_NhanVienBanHang
{
    // Handler co the xu ly yeu cau hoac chuyen cho handler tiep theo.
    public abstract class HelpHandler
    {
        private HelpHandler next;
        public HelpHandler SetNext(HelpHandler handler) { next = handler; return handler; }
        public virtual void ShowHelp() { if (next != null) next.ShowHelp(); else Console.WriteLine("No help available."); }
    }
    public class Dialog : HelpHandler
    {
        private readonly string helpText;
        public Dialog(string helpText) { this.helpText = helpText; }
        public override void ShowHelp() { if (!String.IsNullOrEmpty(helpText)) Console.WriteLine("Dialog help: " + helpText); else base.ShowHelp(); }
    }
    public class Panel : HelpHandler
    {
        private readonly string helpText;
        public Panel(string helpText) { this.helpText = helpText; }
        public override void ShowHelp() { if (!String.IsNullOrEmpty(helpText)) Console.WriteLine("Panel help: " + helpText); else base.ShowHelp(); }
    }
    public class Button : HelpHandler
    {
        private readonly string helpText;
        public Button(string helpText) { this.helpText = helpText; }
        public override void ShowHelp() { if (!String.IsNullOrEmpty(helpText)) Console.WriteLine("Button help: " + helpText); else base.ShowHelp(); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALCHAINOFRESPONSIBILITY PATTERN ===");

            Dialog dialog = new Dialog("General dialog help"); Panel panel = new Panel(null); Button button = new Button(null);
            button.SetNext(panel).SetNext(dialog); button.ShowHelp();
            Button specificButton = new Button("Click to submit the form"); specificButton.SetNext(panel).SetNext(dialog); specificButton.ShowHelp();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
