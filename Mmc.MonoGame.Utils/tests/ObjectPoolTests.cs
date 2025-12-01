namespace Mmc.MonoGame.Utils.Tests;

[TestClass]
public class ObjectPoolTests
{
    private class TestObject
    {
        public int TestValue { get; set; }
    }

    [TestMethod]
    public void GetReturnsNewObjectWhenEmpty()
    {
        var pool = new ObjectPool<TestObject>();

        var obj = pool.Get();

        Assert.IsNotNull(obj);
    }

    [TestMethod]
    public void ReturnAddsObjectBackToPool()
    {
        var pool = new ObjectPool<TestObject>();

        var obj = pool.Get();
        pool.Return(obj);

        var retrieved = pool.Get();

        Assert.AreSame(obj, retrieved);
    }

    [TestMethod]
    public void ReturnDoesNotExceedMaxPoolSize()
    {
        var pool = new ObjectPool<TestObject>(maxPoolSize: 1);

        var obj1 = pool.Get();
        var obj2 = pool.Get();

        pool.Return(obj1);
        pool.Return(obj2);

        var retrieved1 = pool.Get();
        var retrieved2 = pool.Get();

        Assert.AreSame(obj1, retrieved1);
        Assert.AreNotSame(obj2, retrieved2); // these should be brand new instances
    }

    [TestMethod]
    public void ResetActionIsCalledOnReturn()
    {
        bool didReset = false;

        var pool = new ObjectPool<TestObject>(resetAction: (obj) => didReset = true);

        var obj = pool.Get();

        pool.Return(obj);

        Assert.IsTrue(didReset);
    }

    [TestMethod]
    public void CreateFuncIsCalledOnObjectCreation()
    {
        int testValue = 294737;

        var pool = new ObjectPool<TestObject>(createFuncOverride: () => new TestObject() { TestValue = testValue });

        var obj = pool.Get();

        Assert.AreEqual(testValue, obj.TestValue);
    }

    [TestMethod]
    public void PrewarmCreatesObjects()
    {
        const int PrewarmCount = 5;

        var pool = new ObjectPool<TestObject>();

        pool.Prewarm(PrewarmCount);

        Assert.AreEqual(PrewarmCount, pool.ReadyInstanceCount);
    }

    [TestMethod]
    public void PrewarmRespectsMaxPoolSize()
    {
        const int MaxPoolSize = 5;
        const int PrewarmCount = 10;

        var pool = new ObjectPool<TestObject>(maxPoolSize: MaxPoolSize);

        pool.Prewarm(PrewarmCount);

        Assert.AreEqual(MaxPoolSize, pool.ReadyInstanceCount);
    }

    [TestMethod]
    public void ClearPool_RemovesAllObjects()
    {
        var pool = new ObjectPool<TestObject>();
        pool.Prewarm(5);

        pool.ClearPool();

        Assert.AreEqual(0, pool.ReadyInstanceCount);
    }
}
