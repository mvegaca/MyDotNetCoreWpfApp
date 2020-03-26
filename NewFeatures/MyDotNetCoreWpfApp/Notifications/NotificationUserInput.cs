using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyDotNetCoreWpfApp.Models;

namespace MyDotNetCoreWpfApp.Notifications
{
    public class NotificationUserInput : IReadOnlyDictionary<string, string>
    {
        private NotificationUserInputData[] _data;

        public string this[string key]
            => _data.First(i => i.Key == key).Value;

        public IEnumerable<string> Keys
            => _data.Select(i => i.Key);

        public IEnumerable<string> Values
            => _data.Select(i => i.Value);

        public int Count
            => _data.Length;

        public NotificationUserInput(NotificationUserInputData[] data)
        {
            _data = data;
        }

        public bool ContainsKey(string key)
            => _data.Any(i => i.Key == key);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => _data.Select(i => new KeyValuePair<string, string>(i.Key, i.Value)).GetEnumerator();

        public bool TryGetValue(string key, out string value)
        {
            foreach (var item in _data)
            {
                if (item.Key == key)
                {
                    value = item.Value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
