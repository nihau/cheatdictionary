using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatDictionary
{
    /// <summary>
    /// allows to contain same key multiple times
    /// </summary>
    public class CheatDictionary<K, V> : IDictionary<K, V>
    {
        private List<Dictionary<K, V>> _dictionaries;
        public IEqualityComparer<K> Comparer { get; set; }

        public ICollection<K> Keys
        {
            get
            {
                var l = new List<K>();

                foreach (var dic in _dictionaries)
                {
                    l.AddRange(dic.Keys);
                }

                return l;
            }
        }

        public ICollection<V> Values
        {
            get
            {
                var l = new List<V>();

                foreach (var dic in _dictionaries)
                {
                    l.AddRange(dic.Values);
                }

                return l;
            }
        }

        public V this[K key]
        {
            get
            {
                foreach (var dic in _dictionaries)
                {
                    try
                    {
                        return dic[key];
                    }
                    catch (Exception)
                    {

                    }
                }

                throw new KeyNotFoundException();
            }
            set
            {
                foreach (var dic in _dictionaries)
                {
                    try
                    {
                        dic[key] = value;

                        return;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        public CheatDictionary(IEqualityComparer<K> comparer)
        {
            Comparer = comparer;

            _dictionaries = new List<Dictionary<K, V>>();

            AddNewDictionary();
        }

        public CheatDictionary()
            : this(EqualityComparer<K>.Default)
        {
        }

        private void AddNewDictionary()
        {
            _dictionaries.Add(new Dictionary<K, V>(Comparer));
        }

        public void Add(K key, V value)
        {
            foreach (var dic in _dictionaries)
            {
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, value);
                    return;
                }
            }

            AddNewDictionary();
            _dictionaries.Last().Add(key, value);
        }

        public bool ContainsKey(K key)
        {
            foreach (var dic in _dictionaries)
            {
                if (dic.ContainsKey(key))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            foreach (var dic in _dictionaries)
            {
                if (dic.ContainsKey(key))
                {
                    value = dic[key];
                    return true;
                }
            }

            value = default(V);
            return false;
        }

        public void Add(KeyValuePair<K, V> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionaries.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            foreach (var dic in _dictionaries)
            {
                if (((ICollection<KeyValuePair<K, V>>)dic).Contains(item))
                    return true;
            }

            return false;
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            var l = new List<KeyValuePair<K, V>>();

            l.AddRange(_dictionaries.SelectMany(u => u.ToList()));

            l.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _dictionaries.Select(u => u.Count).Sum(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            foreach (var dic in _dictionaries)
            {
                var col = (ICollection<KeyValuePair<K, V>>)dic;

                if (col.Contains(item))
                {
                    return col.Remove(item);
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            foreach (var dic in _dictionaries)
            {
                foreach (var kvp in dic)
                    yield return kvp;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
