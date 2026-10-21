using System;

namespace DPM235417_LamHoangChiHau_RealFacadePattern_NhanVienBanHang
{
    public class VideoFile { public string Name { get; private set; } public string Codec { get; private set; }
        public VideoFile(string name) { Name = name; Codec = name.EndsWith(".ogg") ? "ogg" : "mp4"; } }
    public class CodecFactory { public string Extract(VideoFile file) { Console.WriteLine("Read codec: " + file.Codec); return file.Codec; } }
    public class BitrateReader { public string Read(VideoFile file, string codec) { Console.WriteLine("Read video using " + codec); return "raw(" + file.Name + ")"; }
        public string Convert(string buffer, string codec) { Console.WriteLine("Convert buffer to " + codec); return codec + "(" + buffer + ")"; } }
    public class AudioMixer { public string Fix(string result) { Console.WriteLine("Synchronize audio track."); return result + "+audio"; } }
    // Facade an cac buoc phuc tap cua he thong chuyen doi video.
    public class VideoConverter
    {
        public string Convert(string fileName, string targetCodec)
        {
            VideoFile file = new VideoFile(fileName); CodecFactory factory = new CodecFactory();
            string sourceCodec = factory.Extract(file); BitrateReader reader = new BitrateReader();
            string buffer = reader.Read(file, sourceCodec); string result = reader.Convert(buffer, targetCodec);
            return new AudioMixer().Fix(result);
        }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALFACADE PATTERN ===");
 string output = new VideoConverter().Convert("holiday.ogg", "mp4"); Console.WriteLine("Output: " + output); 
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
