namespace Mmc.MonoGame.Utils
{
    public class FixedObjectPool<T> where T : new()
    {
        private readonly T[] pool;
        private readonly Action<T>? resetAction;
        private int index;

        public FixedObjectPool(int poolSize, Func<T>? createFuncOverride = null, Action<T>? resetAction = null)
        {
            pool = new T[poolSize];

            Func<T> createFunc = createFuncOverride ?? (() => new T());
            for (int i = 0; i < poolSize; i++)
            {
                pool[i] = createFunc();
            }
            index = poolSize - 1;

            this.resetAction = resetAction;
        }

        public bool TryGet(out T? item)
        {
            if (index >= 0)
            {
                item = pool[index];
                index--;
                return true;
            }
            item = default;
            return false;
        }

        public void Return(T item)
        {
            if (index >= pool.Length - 1) return;

            resetAction?.Invoke(item);
            index++;
            pool[index] = item;
        }
    }
}
