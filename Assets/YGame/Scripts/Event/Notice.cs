using System;
using System.Collections.Generic;

namespace YGame.Scripts.Event
{
   /// <summary>
    /// 广播消息
    /// </summary>
    public static class Notice
    {
        private static readonly Dictionary<string, Action> _sEventTable = new Dictionary<string, Action>();

        public static void AddListener(string eventType, Action handler)
        {
            if (!_sEventTable.ContainsKey(eventType))
            {
                _sEventTable.Add(eventType, null);
            }
            _sEventTable[eventType] += handler;
        }

        public static void RemoveListener(string eventType, Action handler)
        {
            if (_sEventTable.ContainsKey(eventType))
            {
                // ReSharper disable once DelegateSubtraction
                _sEventTable[eventType] -= handler;
            }
        }

        public static void RemoveAllListener(string eventType)
        {
            if (_sEventTable.ContainsKey(eventType))
            {
                // ReSharper disable once DelegateSubtraction
                _sEventTable[eventType] = null;
            }
        }
        
        public static void Broadcast(string eventType)
        {
            if (_sEventTable.TryGetValue(eventType, out Action action))
            {
                action?.Invoke();
            }
        }
    }

    public static class Notice<T>
    {
        private static readonly Dictionary<string, Action<T>> _sEventTable = new Dictionary<string, Action<T>>();

        public static void AddListener(string eventType, Action<T> handler)
        {
            if (!_sEventTable.ContainsKey(eventType))
            {
                _sEventTable.Add(eventType, null);
            }
            _sEventTable[eventType] += handler;
        }

        public static void RemoveListener(string eventType, Action<T> handler)
        {
            if (_sEventTable.ContainsKey(eventType))
            {
                // ReSharper disable once DelegateSubtraction
                _sEventTable[eventType] -= handler;
            }
        }

        public static void Broadcast(string eventType, T arg1)
        {
            if (_sEventTable.TryGetValue(eventType, out Action<T> action))
            {
                action?.Invoke(arg1);
            }
        }
    }

    public static class Notice<T, TU>
    {
        private static readonly Dictionary<string, Action<T, TU>> _sEventTable = new Dictionary<string, Action<T, TU>>();

        public static void AddListener(string eventType, Action<T, TU> handler)
        {
            if (!_sEventTable.ContainsKey(eventType))
            {
                _sEventTable.Add(eventType, null);
            }
            _sEventTable[eventType] += handler;
        }

        public static void RemoveListener(string eventType, Action<T, TU> handler)
        {
            if (_sEventTable.ContainsKey(eventType))
            {
                // ReSharper disable once DelegateSubtraction
                _sEventTable[eventType] -= handler;
            }
        }

        public static void Broadcast(string eventType, T arg1, TU arg2)
        {
            if (_sEventTable.TryGetValue(eventType, out Action<T, TU> action))
            {
                action?.Invoke(arg1, arg2);
            }
        }
    }

    public static class Notice<T, TU, TV>
    {
        private static readonly Dictionary<string, Action<T, TU, TV>> _sEventTable =
            new Dictionary<string, Action<T, TU, TV>>();

        public static void AddListener(string eventType, Action<T, TU, TV> handler)
        {
            if (!_sEventTable.ContainsKey(eventType))
            {
                _sEventTable.Add(eventType, null);
            }

            _sEventTable[eventType] += handler;
        }

        public static void RemoveListener(string eventType, Action<T, TU, TV> handler)
        {
            if (_sEventTable.ContainsKey(eventType))
            {
                // ReSharper disable once DelegateSubtraction
                _sEventTable[eventType] -= handler;
            }
        }

        public static void Broadcast(string eventType, T arg1, TU arg2, TV arg3)
        {
            if (_sEventTable.TryGetValue(eventType, out Action<T, TU, TV> action))
            {
                action?.Invoke(arg1, arg2, arg3);
            }
        }
    }
}