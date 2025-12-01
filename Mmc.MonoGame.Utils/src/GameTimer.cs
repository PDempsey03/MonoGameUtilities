using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils
{
    public class GameTimer : IUpdateable
    {
        protected int _updateOrder = 0;
        protected bool _enabled = false;

        protected readonly List<ScheduledAction> scheduledActions = [];
        protected int scheduleActionsCurrentIndex = 0;

        public event EventHandler<EventArgs>? EnabledChanged;
        public event EventHandler<EventArgs>? UpdateOrderChanged;

        public event EventHandler<GameTimerUpdateEventArgs>? Updated;
        public event EventHandler<EventArgs>? TimerCompleted;

        public TimeSpan CurrentTime { get; set; } = TimeSpan.Zero;

        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        public bool Repeating { get; set; } = false;

        public TimeSpan TimeRemaining => Duration - CurrentTime;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    EnabledChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public int UpdateOrder
        {
            get => _updateOrder;
            set
            {
                if (value != _updateOrder)
                {
                    _updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Enabled) return;

            // update to new time
            CurrentTime += gameTime.ElapsedGameTime;

            bool complete = CurrentTime >= Duration;

            if (complete) CurrentTime = Duration;

            // invoke event called on every update cycle
            Updated?.Invoke(this, new GameTimerUpdateEventArgs(CurrentTime, Duration));

            // check for actions called on specific times
            CheckScheduledActions();

            if (complete)
                HandleTimerCompletion();
        }

        protected virtual void CheckScheduledActions()
        {
            while (true)
            {
                var currentScheduledAction = GetCurrentScheduledAction();

                if (!currentScheduledAction.HasValue || CurrentTime < currentScheduledAction.Value.TriggerTime)
                    break;

                currentScheduledAction.Value.Action?.Invoke();
                scheduleActionsCurrentIndex++;
            }
        }

        public void SetDurationInMilliSeconds(double milliSeconds) => Duration = TimeSpan.FromMilliseconds(milliSeconds);

        public void SetDurationInSeconds(double seconds) => SetDurationInMilliSeconds(seconds * 1000);

        public void SetDurationInMinutes(double minutes) => SetDurationInSeconds(minutes * 60);

        public void SetDurationInMinutesAndSeconds(double minutes, double seconds) => SetDurationInSeconds(minutes * 60 + seconds);

        protected virtual void HandleTimerCompletion()
        {
            Reset();
            Enabled = Repeating;
            TimerCompleted?.Invoke(this, new EventArgs());
        }

        public virtual void Start()
        {
            Reset();
            Enabled = true;
        }

        public virtual void Pause()
        {
            Enabled = false;
        }

        public virtual void Resume()
        {
            Enabled = true;
        }

        public virtual void Reset()
        {
            CurrentTime = TimeSpan.Zero;
            Enabled = false;
            scheduleActionsCurrentIndex = 0;
        }

        // allow custom interval actions
        public void ScheduleTimeAction(TimeSpan time, Action action)
        {
            scheduledActions.Add(new ScheduledAction(time, action));

            // resort the list for proper optimization
            scheduledActions.Sort((a, b) => a.TriggerTime.CompareTo(b.TriggerTime));
        }

        public void ClearScheduledTimeActions()
        {
            scheduledActions.Clear();
        }

        protected ScheduledAction? GetCurrentScheduledAction()
        {
            return (scheduleActionsCurrentIndex >= 0 && scheduleActionsCurrentIndex < scheduledActions.Count) ? scheduledActions[scheduleActionsCurrentIndex] : null;
        }

        protected readonly struct ScheduledAction
        {
            public TimeSpan TriggerTime { get; init; }

            public Action Action { get; init; }

            public ScheduledAction(TimeSpan triggerTime, Action action)
            {
                TriggerTime = triggerTime;
                Action = action;
            }
        }
    }
}
