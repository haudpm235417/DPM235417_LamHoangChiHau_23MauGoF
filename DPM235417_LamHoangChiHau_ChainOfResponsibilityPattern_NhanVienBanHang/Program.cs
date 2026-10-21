using System;

namespace DPM235417_LamHoangChiHau_ChainOfResponsibilityPattern_NhanVienBanHang
{
    public class DiscountRequest
    {
        public string OrderCode { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public DiscountRequest(string orderCode, decimal discountPercent)
        { OrderCode = orderCode; DiscountPercent = discountPercent; }
    }

    // Handler: nếu không xử lý được, chuyển yêu cầu cho handler kế tiếp.
    public abstract class ApprovalHandler
    {
        private ApprovalHandler next;
        public ApprovalHandler SetNext(ApprovalHandler handler) { next = handler; return handler; }
        public void Handle(DiscountRequest request)
        {
            if (request.DiscountPercent <= MaximumPercent)
                Console.WriteLine(RoleName + " chap nhan giam " + request.DiscountPercent + "% cho don " + request.OrderCode + ".");
            else if (next != null) next.Handle(request);
            else Console.WriteLine("Tu choi: muc giam " + request.DiscountPercent + "% vuot quyen han.");
        }
        protected abstract decimal MaximumPercent { get; }
        protected abstract string RoleName { get; }
    }

    public class StoreManager : ApprovalHandler
    {
        protected override decimal MaximumPercent { get { return 5m; } }
        protected override string RoleName { get { return "Quan ly cua hang"; } }
    }
    public class SalesDirector : ApprovalHandler
    {
        protected override decimal MaximumPercent { get { return 15m; } }
        protected override string RoleName { get { return "Giam doc kinh doanh"; } }
    }
    public class Owner : ApprovalHandler
    {
        protected override decimal MaximumPercent { get { return 30m; } }
        protected override string RoleName { get { return "Chu cua hang"; } }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: CHAINOFRESPONSIBILITY PATTERN ===");

            ApprovalHandler manager = new StoreManager();
            manager.SetNext(new SalesDirector()).SetNext(new Owner());
            manager.Handle(new DiscountRequest("DH001", 4m));
            manager.Handle(new DiscountRequest("DH002", 12m));
            manager.Handle(new DiscountRequest("DH003", 25m));
            manager.Handle(new DiscountRequest("DH004", 40m));
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
