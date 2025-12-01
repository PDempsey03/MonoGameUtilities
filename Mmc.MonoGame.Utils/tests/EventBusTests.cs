namespace Mmc.MonoGame.Utils.Tests;

[TestClass]
[DoNotParallelize]
public class EventBusTests
{
    public class TestEvent
    {
        public int Value { get; }

        public TestEvent(int value) { Value = value; }
    }

    public class AnotherEvent
    {
        public string Name { get; }
        public AnotherEvent(string name) => Name = name;
    }

    [TestInitialize]
    public void TestInitialize()
    {
        EventBus.RemoveAllSubscriptions();
    }

    [TestMethod]
    public void TestEventCalled()
    {
        const int TestValue = 482374;

        bool callbackOccured = false;

        EventBus.Subscribe<TestEvent>((e) =>
        {
            callbackOccured = true;
            Assert.AreEqual(TestValue, e.Value);
        });

        EventBus.Publish(new TestEvent(TestValue));

        Assert.IsTrue(callbackOccured);
    }

    [TestMethod]
    public void MultipleSubscribersAllCalled()
    {
        const int TotalCallbacks = 5;

        int desiredCallbackCount = 0;
        int callbackCount = 0;

        for (int i = 0; i < TotalCallbacks; i++)
        {
            desiredCallbackCount += i;
            var iCopy = i;
            EventBus.Subscribe<TestEvent>((_) => callbackCount += iCopy);
        }

        EventBus.Publish(new TestEvent(1));

        Assert.AreEqual(desiredCallbackCount, callbackCount);
    }

    [TestMethod]
    public void UnsubscribePreventsCallback()
    {
        bool called = false;
        void callback(TestEvent e) => called = true;

        EventBus.Subscribe<TestEvent>(callback);
        EventBus.Unsubscribe<TestEvent>(callback);

        EventBus.Publish(new TestEvent(0));

        Assert.IsFalse(called);
    }

    [TestMethod]
    public void PublishingWithoutSubscribersDoesNotThrow()
    {
        try
        {
            EventBus.Publish(new TestEvent(0));
        }
        catch
        {
            Assert.Fail("Publish threw an exception with no subscribers.");
        }
    }

    [TestMethod]
    public void DifferentEventTypesDoNotInterfere()
    {
        bool testEventCalled = false;
        bool anotherEventCalled = false;

        EventBus.Subscribe<TestEvent>((_) => testEventCalled = true);
        EventBus.Subscribe<AnotherEvent>((_) => anotherEventCalled = true);

        EventBus.Publish(new TestEvent(123));

        Assert.IsTrue(testEventCalled);
        Assert.IsFalse(anotherEventCalled);

        EventBus.Publish(new AnotherEvent("hi"));

        Assert.IsTrue(anotherEventCalled);
    }

    [TestMethod]
    public void SubscriptionOrderIsPreserved()
    {
        List<int> correctOrder = [1, 2, 3];

        List<int> order = [];

        EventBus.Subscribe<TestEvent>((_) => order.Add(1));
        EventBus.Subscribe<TestEvent>((_) => order.Add(2));
        EventBus.Subscribe<TestEvent>((_) => order.Add(3));

        EventBus.Publish(new TestEvent(0));

        CollectionAssert.AreEqual(correctOrder, order);
    }

    [TestMethod]
    public void RemoveSubscriptionsFromEventRemovesOnlyThatEvent()
    {
        bool testEventCalled = false;
        bool anotherEventCalled = false;

        EventBus.Subscribe<TestEvent>((_) => testEventCalled = true);
        EventBus.Subscribe<AnotherEvent>((_) => anotherEventCalled = true);

        EventBus.RemoveSubscriptionsFromEvent<TestEvent>();

        EventBus.Publish(new TestEvent(0));
        EventBus.Publish(new AnotherEvent("hi"));

        Assert.IsFalse(testEventCalled);
        Assert.IsTrue(anotherEventCalled);
    }
}
