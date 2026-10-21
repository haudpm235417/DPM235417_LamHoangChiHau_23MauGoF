using System;

namespace DPM235417_LamHoangChiHau_RealStatePattern_NhanVienBanHang
{
    public interface IPlayerState { void ClickPlay(AudioPlayer player); void ClickLock(AudioPlayer player); }
    public class AudioPlayer
    {
        private IPlayerState state; public bool Playing { get; set; }
        public AudioPlayer() { state = new ReadyState(); }
        public void SetState(IPlayerState state) { this.state = state; }
        public void ClickPlay() { state.ClickPlay(this); }
        public void ClickLock() { state.ClickLock(this); }
    }
    public class ReadyState : IPlayerState
    {
        public void ClickPlay(AudioPlayer player) { player.Playing = true; player.SetState(new PlayingState()); Console.WriteLine("Player starts playing."); }
        public void ClickLock(AudioPlayer player) { player.SetState(new LockedState()); Console.WriteLine("Player locked."); }
    }
    public class PlayingState : IPlayerState
    {
        public void ClickPlay(AudioPlayer player) { player.Playing = false; player.SetState(new ReadyState()); Console.WriteLine("Player paused."); }
        public void ClickLock(AudioPlayer player) { player.SetState(new LockedState()); Console.WriteLine("Player locked while playing state is retained."); }
    }
    public class LockedState : IPlayerState
    {
        public void ClickPlay(AudioPlayer player) { Console.WriteLine("Unlock the player before playback."); }
        public void ClickLock(AudioPlayer player) { player.SetState(player.Playing ? (IPlayerState)new PlayingState() : new ReadyState()); Console.WriteLine("Player unlocked."); }
    }
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== KET QUA CHAY: REALSTATE PATTERN ===");
 AudioPlayer player = new AudioPlayer(); player.ClickPlay(); player.ClickPlay(); player.ClickLock(); player.ClickPlay(); player.ClickLock(); player.ClickPlay(); 
            // F5 ile calistirirken Console penceresinin hemen kapanmasini engeller.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nSonucu gorduyseniz Enter tusuna basin...");
                Console.ReadLine();
            }
        }
    }
}
