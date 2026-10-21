using System;

namespace DPM235417_LamHoangChiHau_FactoryPattern
{
    // Prototype
    public class Person
    {
        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        // Shallow Copy
        public Person ShallowCopy()
        {
            return (Person)this.MemberwiseClone();
        }

        // Deep Copy
        public Person DeepCopy()
        {
            Person clone = (Person)this.MemberwiseClone();

            // Tạo một IdInfo mới
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);

            // Tạo một chuỗi Name mới
            clone.Name = String.Copy(Name);

            return clone;
        }
    }

    // Đối tượng được tham chiếu bởi Person
    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    // Program
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tạo đối tượng Person ban đầu
            Person p1 = new Person();

            p1.Age = 42;
            p1.BirthDate = Convert.ToDateTime("1977-01-01");
            p1.Name = "Jack Daniels";
            p1.IdInfo = new IdInfo(666);

            // Shallow Copy
            Person p2 = p1.ShallowCopy();

            // Deep Copy
            Person p3 = p1.DeepCopy();

            // Hiển thị giá trị ban đầu
            Console.WriteLine("Original values of p1, p2, p3:");

            Console.WriteLine("   p1 instance values:");
            DisplayValues(p1);

            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);

            Console.WriteLine("   p3 instance values:");
            DisplayValues(p3);

            // Thay đổi p1
            p1.Age = 32;
            p1.BirthDate = Convert.ToDateTime("1900-01-01");
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 7878;

            // Hiển thị sau khi thay đổi
            Console.WriteLine(
                "\nValues of p1, p2 and p3 after changes to p1:"
            );

            Console.WriteLine("   p1 instance values:");
            DisplayValues(p1);

            Console.WriteLine(
                "   p2 instance values (reference values have changed):"
            );
            DisplayValues(p2);

            Console.WriteLine(
                "   p3 instance values (everything was kept the same):"
            );
            DisplayValues(p3);

            Console.ReadKey();
        }

        // Hiển thị thông tin Person
        public static void DisplayValues(Person p)
        {
            Console.WriteLine(
                "      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                p.Name,
                p.Age,
                p.BirthDate
            );

            Console.WriteLine(
                "      ID#: {0:d}",
                p.IdInfo.IdNumber
            );
        }
    }
}