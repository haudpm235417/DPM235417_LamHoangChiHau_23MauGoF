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

        // Business Logic
        public void SomeBusinessLogic()
        {
            //....
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lấy Singleton instance lần thứ nhất
            Singleton s1 = Singleton.GetInstance();

            // Lấy Singleton instance lần thứ hai
            Singleton s2 = Singleton.GetInstance();

            // Kiểm tra hai biến có cùng trỏ đến một instance hay không
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

            // Gọi business logic
            s1.SomeBusinessLogic();

            Console.ReadKey();
        }
    }
}