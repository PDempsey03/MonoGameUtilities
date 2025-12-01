namespace Mmc.MonoGame.Utils.Tests;

[TestClass]
public class FixedObjectPoolTests
{
    private class TestObject
    {
        public int Value { get; set; }
    }

    [TestMethod]
    public void TryGetReturnsTrueWhenPoolHasObjects()
    {
        var pool = new FixedObjectPool<TestObject>(poolSize: 3);

        bool result = pool.TryGet(out var item);

        Assert.IsTrue(result);
        Assert.IsNotNull(item);
    }

    [TestMethod]
    public void TryGetReturnsFalseWhenPoolIsEmpty()
    {
        var pool = new FixedObjectPool<TestObject>(poolSize: 1);

        // first succeeds
        pool.TryGet(out _);

        // second fails
        bool result = pool.TryGet(out var item);

        Assert.IsFalse(result);
        Assert.IsNull(item);
    }

    [TestMethod]
    public void ReturnAddsObjectBackToPool()
    {
        var pool = new FixedObjectPool<TestObject>(poolSize: 1);

        pool.TryGet(out var obj);

        pool.Return(obj!);

        bool result = pool.TryGet(out var retrieved);

        Assert.IsTrue(result);
        Assert.AreSame(obj, retrieved);
    }

    [TestMethod]
    public void ReturnDoesNotOverflowPool()
    {
        var pool = new FixedObjectPool<TestObject>(poolSize: 1);

        pool.TryGet(out var obj1);

        pool.Return(obj1!);

        var extra = new TestObject();
        pool.Return(extra);

        pool.TryGet(out var retrieved);

        Assert.AreSame(obj1, retrieved);
    }

    [TestMethod]
    public void ResetActionIsCalledOnReturn()
    {
        bool didReset = false;

        var pool = new FixedObjectPool<TestObject>(poolSize: 1, resetAction: (o) => didReset = true);

        pool.TryGet(out var item);
        pool.Return(item!);

        Assert.IsTrue(didReset);
    }

    [TestMethod]
    public void PoolInitializesObjectsOnConstruction()
    {
        const int PoolSize = 5;

        var pool = new FixedObjectPool<TestObject>(poolSize: PoolSize);

        int count = 0;
        while (pool.TryGet(out _)) count++;

        Assert.AreEqual(PoolSize, count);
    }
}
