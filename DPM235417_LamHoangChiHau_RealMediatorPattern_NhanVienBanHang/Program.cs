using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealMediatorPattern_NhanVienBanHang
{
    public interface IChatMediator { void SendMessage(string message, User sender); void AddUser(User user); }
    public class ChatRoom : IChatMediator
    {
        private readonly List<User> users = new List<User>();
        public void AddUser(User user) { users.Add(user); }
        public void SendMessage(string message, User sender)
        { foreach (User user in users) if (user != sender) user.Receive(sender.Name + ": " + message); }
    }
    public abstract class User
    {
        protected readonly IChatMediator mediator; public string Name { get; private set; }
        protected User(IChatMediator mediator, string name) { this.mediator = mediator; Name = name; }
        public void Send(string message) { Console.WriteLine(Name + " sends: " + message); mediator.SendMessage(message, this); }
        public void Receive(string message) { Console.WriteLine(Name + " receives: " + message); }
    }
    public class ChatUser : User { public ChatUser(IChatMediator mediator, string name) : base(mediator, name) { } }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALMEDIATOR PATTERN ===");

            IChatMediator room = new ChatRoom(); User alice = new ChatUser(room, "Alice"); User bob = new ChatUser(room, "Bob"); User carol = new ChatUser(room, "Carol");
            room.AddUser(alice); room.AddUser(bob); room.AddUser(carol); alice.Send("Hello everyone!"); bob.Send("Hi Alice!");
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
