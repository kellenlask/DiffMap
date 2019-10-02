using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiffMap
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DiffMap<TKey, TValue> : IDictionary, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        private readonly Func<TKey, TValue, TValue, TValue> _conflictResolver;
        private readonly Dictionary<TKey, TValue> _map = new Dictionary<TKey, TValue>();

        public int Count => _map.Count;
        public bool IsSynchronized => ((ICollection) _map).IsSynchronized;
        public object SyncRoot => ((ICollection) _map).SyncRoot;
        public bool IsReadOnly => ((IDictionary<TKey, TValue>) _map).IsReadOnly;
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _map.Keys;
        public ICollection<TValue> Values => _map.Values;
        public ICollection<TKey> Keys => _map.Keys;
        ICollection IDictionary.Values => _map.Values;
        ICollection IDictionary.Keys => _map.Keys;
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _map.Values;


        public DiffMap(Func<TKey, TValue, TValue, TValue> conflictResolver) => _conflictResolver = conflictResolver;


        public object this[object key] {
            get => ((IDictionary) _map)[key];
            set => ((IDictionary) _map)[key] = value;
        }


        public TValue this[TKey key] {
            get => _map[key];
            set => _map[key] = value;
        }


        public bool Contains(object key) => ((IDictionary) _map).Contains(key);


        public bool Contains(KeyValuePair<TKey, TValue> item) => _map.Contains(item);


        public bool ContainsKey(TKey key) => _map.ContainsKey(key);


        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary) _map).GetEnumerator();


        public bool IsFixedSize => ((IDictionary) _map).IsFixedSize;


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _map.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _map).GetEnumerator();


        public void Add(KeyValuePair<TKey, TValue> item) => _map.Add(
            item.Key,
            ContainsKey(item.Key) ? _conflictResolver(item.Key, this[item.Key], item.Value) : item.Value
        );


        public void Add(object key, object value) => ((IDictionary) _map).Add(
            key,
            Contains(key) ? _conflictResolver((TKey) key, this[(TKey) key], (TValue) value) : value
        );


        public void Add(TKey key, TValue value) => _map.Add(
            key,
            ContainsKey(key) ? _conflictResolver(key, this[key], value) : value
        );


        public void Remove(object key) => ((IDictionary) _map).Remove(key);


        public bool Remove(KeyValuePair<TKey, TValue> item) => _map.Remove(item.Key);


        public bool Remove(TKey key) => _map.Remove(key);


        public void Clear() => _map.Clear();


        public void CopyTo(
            KeyValuePair<TKey, TValue>[] array,
            int arrayIndex
        ) => ((IDictionary<TKey, TValue>) _map).CopyTo(array, arrayIndex);


        public void CopyTo(Array array, int index) => ((ICollection) _map).CopyTo(array, index);


        public bool TryGetValue(TKey key, out TValue value) => _map.TryGetValue(key, out value);
    }
}
