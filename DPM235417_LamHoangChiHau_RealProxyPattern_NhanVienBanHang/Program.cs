using System;
using System.Collections.Generic;

namespace DPM235417_LamHoangChiHau_RealProxyPattern_NhanVienBanHang
{
    public interface IVideoService { string GetVideoInfo(string id); string DownloadVideo(string id); }
    public class YouTubeService : IVideoService
    {
        public string GetVideoInfo(string id) { Console.WriteLine("Request info from remote service: " + id); return "Video " + id; }
        public string DownloadVideo(string id) { Console.WriteLine("Download data from remote service: " + id); return "<video-bytes:" + id + ">"; }
    }
    // Proxy luu cache de tranh lap lai cac yeu cau cung video.
    public class YouTubeCacheProxy : IVideoService
    {
        private readonly IVideoService service;
        private readonly Dictionary<string, string> infoCache = new Dictionary<string, string>();
        private readonly Dictionary<string, string> videoCache = new Dictionary<string, string>();
        public YouTubeCacheProxy(IVideoService service) { this.service = service; }
        public string GetVideoInfo(string id)
        { if (!infoCache.ContainsKey(id)) infoCache[id] = service.GetVideoInfo(id); else Console.WriteLine("Info cache hit: " + id); return infoCache[id]; }
        public string DownloadVideo(string id)
        { if (!videoCache.ContainsKey(id)) videoCache[id] = service.DownloadVideo(id); else Console.WriteLine("Video cache hit: " + id); return videoCache[id]; }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALPROXY PATTERN ===");

            IVideoService service = new YouTubeCacheProxy(new YouTubeService());
            Console.WriteLine(service.GetVideoInfo("abc")); Console.WriteLine(service.GetVideoInfo("abc"));
            Console.WriteLine(service.DownloadVideo("abc")); Console.WriteLine(service.DownloadVideo("abc"));
        
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
