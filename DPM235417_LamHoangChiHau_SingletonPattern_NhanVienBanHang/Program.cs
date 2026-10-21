
using System;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Singleton
    public sealed class Singleton
    {
        // Constructor private để không cho phép tạo đối tượng bằng new
        private Singleton()
        {
        }

        // Biến static lưu instance duy nhất
        private static Singleton _instance;

        // Phương thức lấy instance Singleton
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }

            return _instance;
        }

        // Chức năng quản lý thông tin cửa hàng
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }

        public void HienThiThongTinCuaHang()
        {
            Console.WriteLine("===== THÔNG TIN CỬA HÀNG =====");
            Console.WriteLine("Tên cửa hàng: " + TenCuaHang);
            Console.WriteLine("Địa chỉ: " + DiaChi);
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lấy Singleton instance lần thứ nhất
            Singleton s1 = Singleton.GetInstance();

            // Thiết lập thông tin cửa hàng
            s1.TenCuaHang = "Cửa hàng Nông Dược";
            s1.DiaChi = "An Giang";

            // Lấy Singleton instance lần thứ hai
            Singleton s2 = Singleton.GetInstance();

            // Hiển thị thông tin từ instance thứ hai
            s2.HienThiThongTinCuaHang();

            // Kiểm tra hai biến có cùng trỏ đến một instance hay không
            Console.WriteLine();

            if (s1 == s2)
            {
                Console.WriteLine(
                    "Singleton works, both variables contain the same instance."
                );
            }
            else
            {
                Console.WriteLine(
                    "Singleton failed, variables contain different instances."
                );
            }

            Console.ReadKey();
        }
    }
}
