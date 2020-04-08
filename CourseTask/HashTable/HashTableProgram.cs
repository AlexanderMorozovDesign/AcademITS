using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public interface ICollection<T>
    {
        void Insert(string key, string value);
        void Delete(string key);
        string Search(string key);
    }

    class HashTable<T> : ICollection<T>
    {
        private T[] data;
        private int size;
        private readonly byte max_size = 255;
        private Dictionary<int, List<Item>> _items = null;
        public IReadOnlyCollection<KeyValuePair<int, List<Item>>> Items => _items?.ToList()?.AsReadOnly();

        public HashTable()
        {
            _items = new Dictionary<int, List<Item>>(size);
        }

        public void Insert(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length > max_size)
            {
                throw new ArgumentException($"Макс. длина ключа составляет {max_size} символов", nameof(key));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var item = new Item(key, value);
            var hash = GetHash(item.Key);

            List<Item> hashTableItem = null;
            if (_items.ContainsKey(hash))
            {
                hashTableItem = _items[hash];

                var oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);

                if (oldElementWithKey != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должет быть уникален");
                }
                _items[hash].Add(item);
            }
            else
            {
                hashTableItem = new List<Item> { item };
                _items.Add(hash, hashTableItem);
            }
        }

        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length > max_size)
            {
                throw new ArgumentException($"Макс. длина ключа составляет {max_size} символов", nameof(key));
            }

            var hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return;
            }

            var hashTableItem = _items[hash];
            var item = hashTableItem.SingleOrDefault(i => i.Key == key);

            if (item != null)
            {
                hashTableItem.Remove(item);
            }
        }

        public string Search(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length > max_size)
            {
                throw new ArgumentException($"Макс. длина ключа составляет {max_size} символов", nameof(key));
            }

            var hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return null;
            }

            var hashTableItem = _items[hash];

            if (hashTableItem != null)
            {
                var item = hashTableItem.SingleOrDefault(i => i.Key == key);
                if (item != null)
                {
                    return item.Value;
                }
            }

            return null;
        }

        private int GetHash(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length > max_size)
            {
                throw new ArgumentException($"Макс. длина ключа составляет {max_size} символов", nameof(value));
            }

            var hash = value.Length;

            return hash;
        }

        public void ShowHashTable(HashTable<T> hashTable, string title)
        {
            if (hashTable == null)
            {
                throw new ArgumentNullException(nameof(hashTable));
            }
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }
            Console.WriteLine(title);

            foreach (var item in hashTable.Items)
            {
                Console.WriteLine(item.Key);
                foreach (var value in item.Value)
                {
                    Console.WriteLine($"\t{value.Key} - {value.Value}");
                }
            }

            Console.WriteLine();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                yield return data[i];
            }
        }

    }

    class HashTableProgram
    {
        static void Main(string[] args)
        {
            var hashTable = new HashTable<string>();
            hashTable.Insert("1", "asfdgfghdfgsfwfwfwfwef");
            hashTable.Insert("2", "cvcvbcbcbcbcbcbcbcbcvb");
            hashTable.Insert("3", "lklklklklklklklklklklk");

            hashTable.ShowHashTable(hashTable, "Хен-таблица");
            Console.WriteLine();

            hashTable.Delete("2");
            hashTable.ShowHashTable(hashTable, "Хен-таблица");
            Console.WriteLine();

            Console.WriteLine("Поиск:");
            var text = hashTable.Search("3");
            Console.WriteLine(text);
        }
    }
}
