using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Homework03
{
    public class BinarySearchTreePersistable<TKey, TValue> : IDictionaryPersistable<TKey, TValue>
    {
        public void Serialize(string path, IDictionary<TKey, TValue> tree)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tree);
            }
        }

        public IDictionary<TKey, TValue> Deserialize(string path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (IDictionary<TKey, TValue>)formatter.Deserialize(fs);
            }
        }
    }
}