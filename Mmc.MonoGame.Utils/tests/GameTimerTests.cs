using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils.Tests.Extensions;
using System.Diagnostics;

namespace Mmc.MonoGame.Utils.Tests
{
    [TestClass]
    public sealed class GameTimerTests
    {
        private static void RunNonLoopingTimer(GameTimer timer, TimeSpan dt, int maxUpdates)
        {
            TimeSpan total = TimeSpan.Zero;

            timer.Start();

            int currentUpdate;
            for (currentUpdate = 0; currentUpdate < maxUpdates && timer.Enabled; currentUpdate++)
            {
                total += dt;
                timer.Update(new GameTime(total, dt));
            }

            if (currentUpdate == maxUpdates)
            {
                Assert.Fail("Max update count reached, something went wrong");
            }
        }

        private static void RunLoopingTimer(GameTimer timer, TimeSpan dt, int maxRepeats, int maxUpdates)
        {
            TimeSpan total = TimeSpan.Zero;

            timer.Start();

            int repeatCount;
            for (repeatCount = 0; repeatCount < maxRepeats && timer.Enabled; repeatCount++)
            {
                int currentUpdate;
                for (currentUpdate = 0; currentUpdate < maxUpdates && timer.Enabled; currentUpdate++)
                {
                    total += dt;
                    timer.Update(new GameTime(total, dt));
                }

                if (currentUpdate == maxUpdates)
                {
                    Assert.Fail("Max update count reached, something went wrong");
                }
            }

            if (repeatCount == maxRepeats)
            {
                Assert.Fail("Max repeat count reached, something went wrong");
            }
        }

        [TestMethod]
        public void Update_WhenCalled_InvokesUpdatedEvent()
        {
            TimeSpan dt = TimeSpan.FromSeconds(1f / 16f);

            GameTimer timer = new GameTimer();
            timer.Duration = TimeSpan.FromSeconds(1);

            bool wasUpdateEventCalled = false;

            timer.Updated += (s, e) => wasUpdateEventCalled = true;

            int maxUpdateCount = 1000;

            RunNonLoopingTimer(timer, dt, maxUpdateCount);

            Assert.IsTrue(wasUpdateEventCalled, "Update event was never called");
        }

        [TestMethod]
        public void Update_WhenTimerCompletes_InvokesTimerCompletedEvent()
        {
            TimeSpan dt = TimeSpan.FromSeconds(1f / 16f);

            GameTimer timer = new GameTimer();
            timer.Duration = TimeSpan.FromSeconds(1);

            bool wasCompletedEventCalled = false;

            timer.TimerCompleted += (s, e) => wasCompletedEventCalled = true;

            int maxUpdateCount = 1000;

            RunNonLoopingTimer(timer, dt, maxUpdateCount);

            Assert.IsTrue(wasCompletedEventCalled, "Completed event was never called");
        }

        [TestMethod]
        public void Update_WithDeltaTimes_AccumulatesCorrectly()
        {
            const double DurationSeconds = .5;
            TimeSpan totalTime = TimeSpan.Zero;

            const double DtSeconds = 1f / 60f;// simulate 60 fps
            TimeSpan dt = TimeSpan.FromSeconds(DtSeconds);

            const double Epsilon = DtSeconds / 100f; // give 1% error on the 60fps target

            GameTimer timer = new GameTimer();

            timer.SetDurationInSeconds(DurationSeconds);

            int updateCount = 0;
            timer.Updated += (s, e) =>
            {
                updateCount++;

                Debug.WriteLine($"Current Time: {e.CurrentTime.TotalMilliseconds}\n" +
                    $"Duration: {e.Duration.TotalMilliseconds}\n" +
                    $"Percent Complete: {e.CurrentTime / e.Duration * 100:f2}%\n" +
                    "----------------------------");

                Assert.IsTrue(Math.Min(DtSeconds * updateCount, DurationSeconds).AreNearlyEqual(e.CurrentTime.TotalSeconds, Epsilon));
            };

            int maxUpdateCount = 1000;

            RunNonLoopingTimer(timer, dt, maxUpdateCount);
        }

        [TestMethod]
        public void Update_WhenScheduledActionTimeReached_ExecutesAction()
        {
            TimeSpan dt = TimeSpan.FromSeconds(1f / 60f);

            GameTimer timer = new GameTimer();

            const int TotalSeconds = 2;
            timer.Duration = TimeSpan.FromSeconds(TotalSeconds);

            int scheduledActionsCallBackCount = 0;
            void scheduledAction() => scheduledActionsCallBackCount++;

            const int ScheduledActions = 30;

            for (int i = 0; i < ScheduledActions; i++)
            {
                TimeSpan scheduledTime = TimeSpan.FromSeconds((float)i / ScheduledActions * (float)TotalSeconds);
                Debug.WriteLine($"Scheduled time at {scheduledTime.TotalSeconds} seconds.");
                timer.ScheduleTimeAction(scheduledTime, scheduledAction);
            }

            int maxUpdateCount = 1000;

            RunNonLoopingTimer(timer, dt, maxUpdateCount);

            Assert.AreEqual(ScheduledActions, scheduledActionsCallBackCount);
        }

        [TestMethod]
        public void Update_WhenTimerIsRepeating_ResetsAfterCompletion()
        {
            TimeSpan dt = TimeSpan.FromSeconds(1f / 16f);

            GameTimer timer = new GameTimer();
            timer.Repeating = true;
            timer.Duration = TimeSpan.FromSeconds(1);

            const int TotalTimerCompletionsDesired = 5;
            int timesTimerCompleted = 0;

            timer.TimerCompleted += (s, e) =>
            {
                timesTimerCompleted++;
                if (timesTimerCompleted == TotalTimerCompletionsDesired)
                {
                    timer.Repeating = false;
                    timer.Reset();
                }
            };

            int maxUpdateCount = 1000;

            RunLoopingTimer(timer, dt, TotalTimerCompletionsDesired + 1, maxUpdateCount);

            Assert.AreEqual(TotalTimerCompletionsDesired, timesTimerCompleted);
        }
    }
}