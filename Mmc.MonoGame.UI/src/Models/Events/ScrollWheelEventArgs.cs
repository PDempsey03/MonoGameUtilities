namespace Mmc.MonoGame.UI.Models.Events
{
    public class ScrollWheelEventArgs : EventArgs
    {
        public int ScrollDelta { get; init; }

        public ScrollWheelEventArgs(int scrollDelta)
        {
            ScrollDelta = scrollDelta;
        }
    }
}
