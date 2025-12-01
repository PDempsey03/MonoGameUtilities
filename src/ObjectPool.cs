namespace Mmc.MonoGame.Utils
{
    public class ObjectPool<T> where T : new()
    {
        private readonly Stack<T> pool;
        private readonly Func<T> createFunc;
        private readonly Action<T>? resetAction;
        private readonly int maxPoolSize;

        public int ReadyInstanceCount => pool.Count;

        public ObjectPool(int maxPoolSize = int.MaxValue, Func<T>? createFuncOverride = null, Action<T>? resetAction = null)
        {
            pool = new();
            this.maxPoolSize = maxPoolSize;
            createFunc = createFuncOverride ?? (() => new T());
            this.resetAction = resetAction;
        }

        public T Get()
        {
            return pool.Count > 0 ? pool.Pop() : createFunc();
        }

        public void Return(T item)
        {
            resetAction?.Invoke(item);
            if (pool.Count < maxPoolSize)
                pool.Push(item);
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count && i < maxPoolSize; i++)
            {
                pool.Push(createFunc());
            }
        }

        public void ClearPool()
        {
            pool.Clear();
        }
    }
}
