using System.Collections.Generic;

namespace Homework03
{
    public interface IDictionaryPersistable<TKey, TValue>
    {
        void Serialize(string path, IDictionary<TKey, TValue> tree);
        IDictionary<TKey, TValue> Deserialize(string path);
    }
}