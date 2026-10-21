using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealObserverPattern_NhanVienBanHang
{
    public interface IEventListener { void Update(string fileName); }
    public class EmailAlertsListener : IEventListener
    { private readonly string email; public EmailAlertsListener(string email) { this.email = email; }
      public void Update(string fileName) { Console.WriteLine("Email sent to " + email + ": file changed -> " + fileName); } }
    public class LoggingListener : IEventListener
    { public void Update(string fileName) { Console.WriteLine("Log: file operation on " + fileName); } }
    public class EventManager
    {
        private readonly Dictionary<string, List<IEventListener>> listeners = new Dictionary<string, List<IEventListener>>();
        public void Subscribe(string eventType, IEventListener listener)
        { if (!listeners.ContainsKey(eventType)) listeners[eventType] = new List<IEventListener>(); listeners[eventType].Add(listener); }
        public void Notify(string eventType, string fileName)
        { if (!listeners.ContainsKey(eventType)) return; foreach (IEventListener listener in listeners[eventType]) listener.Update(fileName); }
    }
    public class Editor
    {
        public EventManager Events { get; private set; }
        public Editor() { Events = new EventManager(); }
        public void OpenFile(string name) { Console.WriteLine("Open " + name); Events.Notify("open", name); }
        public void SaveFile(string name) { Console.WriteLine("Save " + name); Events.Notify("save", name); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALOBSERVER PATTERN ===");

            Editor editor = new Editor(); editor.Events.Subscribe("open", new LoggingListener());
            editor.Events.Subscribe("save", new EmailAlertsListener("admin@example.com"));
            editor.OpenFile("report.txt"); editor.SaveFile("report.txt");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
