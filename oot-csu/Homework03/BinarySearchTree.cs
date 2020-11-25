using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Homework03
{
    public class BinarySearchTree<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly List<Node> _tree;

        public BinarySearchTree()
        {
            _tree = new List<Node>();
        }

        private Node GetRootNode()
        {
            return _tree.FirstOrDefault();
        }

        public void Add(TKey key, TValue value)
        {
            var item = new KeyValuePair<TKey, TValue>(key, value);
            var node = new Node() { Data = item };

            Insert(GetRootNode(), node);

            _tree.Add(node);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        private Node Insert(Node parent, Node node)
        {
            if (parent == null)
                return node;

            if (Comparer<TKey>.Default.Compare(node.Data.Key, parent.Data.Key) == -1)
            {
                parent.Left = Insert(parent.Left, node);
            }
            else
            {
                parent.Right = Insert(parent.Right, node);
            }

            return parent;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var result = new List<KeyValuePair<TKey, TValue>>();
            PreOrderTraversal(result, GetRootNode());
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void PreOrderTraversal(List<KeyValuePair<TKey, TValue>> list, Node node)
        {
            if (node == null)
                return;

            list.Add(new KeyValuePair<TKey, TValue>(node.Data.Key, node.Data.Value));
            PreOrderTraversal(list, node.Left);
            PreOrderTraversal(list, node.Right);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }

        public bool ContainsKey(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new System.NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }

        private class Node
        {
            public KeyValuePair<TKey, TValue> Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}