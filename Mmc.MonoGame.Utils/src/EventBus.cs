namespace Mmc.MonoGame.Utils
{
    public static class EventBus
    {
        private static Dictionary<Type, List<Delegate>> subscriptions = new();

        public static void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : class
        {
            var type = typeof(TEvent);

            if (!subscriptions.TryGetValue(type, out var list))
            {
                list = [];
                subscriptions[type] = list;
            }

            list.Add(callback);
        }

        public static void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : class
        {
            var type = typeof(TEvent);

            if (subscriptions.TryGetValue(type, out var list))
            {
                list.Remove(callback);
            }
        }

        public static void Publish<TEvent>(TEvent evt) where TEvent : class
        {
            var type = typeof(TEvent);

            if (!subscriptions.TryGetValue(type, out var list)) return;

            foreach (var callback in list)
            {
                ((Action<TEvent>)callback)?.Invoke(evt);
            }
        }

        public static void RemoveSubscriptionsFromEvent<TEvent>()
        {
            var type = typeof(TEvent);

            subscriptions.Remove(type);
        }

        public static void RemoveAllSubscriptions()
        {
            subscriptions.Clear();
        }
    }
}
