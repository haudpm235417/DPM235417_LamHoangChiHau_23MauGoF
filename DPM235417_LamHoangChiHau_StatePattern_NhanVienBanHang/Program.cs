using System;

namespace DPM235417_LamHoangChiHau_StatePattern_NhanVienBanHang
{
    public interface IOrderState
    {
        string Name { get; }
        void Advance(Order order);
    }

    // Context ủy quyền hành vi chuyển trạng thái cho đối tượng state hiện tại.
    public class Order
    {
        private IOrderState state;
        public Order() { state = new DraftState(); }
        public string CurrentState { get { return state.Name; } }
        public void SetState(IOrderState newState) { state = newState; Console.WriteLine("Trang thai don hang: " + state.Name); }
        public void Advance() { state.Advance(this); }
    }

    public class DraftState : IOrderState
    {
        public string Name { get { return "Moi tao"; } }
        public void Advance(Order order) { Console.WriteLine("Thanh toan thanh cong."); order.SetState(new PaidState()); }
    }
    public class PaidState : IOrderState
    {
        public string Name { get { return "Da thanh toan"; } }
        public void Advance(Order order) { Console.WriteLine("Ban giao don hang cho van chuyen."); order.SetState(new ShippedState()); }
    }
    public class ShippedState : IOrderState
    {
        public string Name { get { return "Dang giao"; } }
        public void Advance(Order order) { Console.WriteLine("Khach da nhan hang."); order.SetState(new DeliveredState()); }
    }
    public class DeliveredState : IOrderState
    {
        public string Name { get { return "Da giao"; } }
        public void Advance(Order order) { Console.WriteLine("Don hang da ket thuc, khong the chuyen tiep."); }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: STATE PATTERN ===");

            Order order = new Order();
            Console.WriteLine("Trang thai don hang: " + order.CurrentState);
            order.Advance(); order.Advance(); order.Advance(); order.Advance();
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
