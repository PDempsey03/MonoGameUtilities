namespace Mmc.MonoGame.Utils
{
    public class GameTimerUpdateEventArgs : EventArgs
    {
        public TimeSpan CurrentTime { get; init; }

        public TimeSpan Duration { get; init; }

        public GameTimerUpdateEventArgs(TimeSpan currentTime, TimeSpan duration)
        {
            CurrentTime = currentTime;
            Duration = duration;
        }
    }
}
