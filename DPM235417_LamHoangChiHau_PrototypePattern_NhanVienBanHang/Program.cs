
using System;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Prototype
    public class SanPham
    {
        public int MaSanPham;
        public string TenSanPham;
        public double DonGia;
        public ThongTinSanPham ThongTin;

        // Shallow Copy
        public SanPham ShallowCopy()
        {
            return (SanPham)this.MemberwiseClone();
        }

        // Deep Copy
        public SanPham DeepCopy()
        {
            SanPham clone = (SanPham)this.MemberwiseClone();

            // Tạo một ThongTinSanPham mới
            clone.ThongTin = new ThongTinSanPham(
                ThongTin.NhaSanXuat,
                ThongTin.LoaiSanPham
            );

            // Tạo chuỗi TenSanPham mới
            clone.TenSanPham = String.Copy(TenSanPham);

            return clone;
        }
    }

    // Đối tượng được tham chiếu bởi SanPham
    public class ThongTinSanPham
    {
        public string NhaSanXuat;
        public string LoaiSanPham;

        public ThongTinSanPham(string nhaSanXuat, string loaiSanPham)
        {
            this.NhaSanXuat = nhaSanXuat;
            this.LoaiSanPham = loaiSanPham;
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tạo sản phẩm ban đầu
            SanPham sp1 = new SanPham();

            sp1.MaSanPham = 101;
            sp1.TenSanPham = "Thuốc trừ sâu";
            sp1.DonGia = 150000;
            sp1.ThongTin = new ThongTinSanPham(
                "Công ty Nông Dược ABC",
                "Thuốc bảo vệ thực vật"
            );

            // Shallow Copy
            SanPham sp2 = sp1.ShallowCopy();

            // Deep Copy
            SanPham sp3 = sp1.DeepCopy();

            // Hiển thị giá trị ban đầu
            Console.WriteLine("===== GIÁ TRỊ BAN ĐẦU =====");

            Console.WriteLine("\nSản phẩm sp1:");
            DisplayValues(sp1);

            Console.WriteLine("\nSản phẩm sp2 - Shallow Copy:");
            DisplayValues(sp2);

            Console.WriteLine("\nSản phẩm sp3 - Deep Copy:");
            DisplayValues(sp3);

            // Thay đổi sp1
            sp1.MaSanPham = 202;
            sp1.TenSanPham = "Phân bón NPK";
            sp1.DonGia = 300000;
            sp1.ThongTin.NhaSanXuat = "Công ty Nông Dược XYZ";
            sp1.ThongTin.LoaiSanPham = "Phân bón";

            // Hiển thị sau khi thay đổi
            Console.WriteLine(
                "\n===== SAU KHI THAY ĐỔI sp1 ====="
            );

            Console.WriteLine("\nSản phẩm sp1:");
            DisplayValues(sp1);

            Console.WriteLine(
                "\nSản phẩm sp2 - Shallow Copy:"
            );
            DisplayValues(sp2);

            Console.WriteLine(
                "\nSản phẩm sp3 - Deep Copy:"
            );
            DisplayValues(sp3);
            Console.ReadKey();
        }

        // Hiển thị thông tin sản phẩm
        public static void DisplayValues(SanPham sp)
        {
            Console.WriteLine(
                "   Mã sản phẩm: {0}",
                sp.MaSanPham
            );

            Console.WriteLine(
                "   Tên sản phẩm: {0}",
                sp.TenSanPham
            );

            Console.WriteLine(
                "   Đơn giá: {0} VNĐ",
                sp.DonGia
            );

            Console.WriteLine(
                "   Nhà sản xuất: {0}",
                sp.ThongTin.NhaSanXuat
            );

            Console.WriteLine(
                "   Loại sản phẩm: {0}",
                sp.ThongTin.LoaiSanPham
            );
        }
    }
}
