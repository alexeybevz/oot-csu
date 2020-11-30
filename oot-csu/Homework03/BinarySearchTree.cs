using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Homework03
{
    public class BinarySearchTree<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Node _root;

        public ICollection<TKey> Keys => this.Select(x => x.Key).ToList();
        public ICollection<TValue> Values => this.Select(x => x.Value).ToList();

        public void Add(TKey key, TValue value)
        {
            var item = new KeyValuePair<TKey, TValue>(key, value);
            var node = new Node() { Data = item };

            if (_root == null)
            {
                _root = node;
                return;
            }

            Insert(_root, node);
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
                parent.Left.Parent = parent;
            }
            else
            {
                parent.Right = Insert(parent.Right, node);
                parent.Right.Parent = parent;
            }

            return parent;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var result = new List<KeyValuePair<TKey, TValue>>();
            PreOrderTraversal(result, _root);
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

        private Node Search(Node parent, TKey searchKey)
        {
            if (parent == null || Comparer<TKey>.Default.Compare(searchKey, parent.Data.Key) == 0)
                return parent;

            if (Comparer<TKey>.Default.Compare(searchKey, parent.Data.Key) == -1)
                return Search(parent.Left, searchKey);
            else
                return Search(parent.Right, searchKey);
        }

        private Node Next(TKey key)
        {
            Node current = _root;
            Node successor = null;

            while (current != null)
            {
                if (Comparer<TKey>.Default.Compare(current.Data.Key, key) > 0)
                {
                    successor = current;
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return successor;
        }

        public bool Remove(TKey key)
        {
            var node = Search(_root, key);
            var parent = node.Parent;

            // I: удаляемый элемент - лист
            if (node.Left == null && node.Right == null)
            {
                if (parent.Left == node)
                    parent.Left = null;
                if (parent.Right == node)
                    parent.Right = null;
                return true;
            }

            // II: удаляемый элемент - имеет одного потомка
            if (node.Left == null || node.Right == null)
            {
                if (node.Left == null)
                {
                    if (parent.Left == node)
                        parent.Left = node.Right;
                    else
                        parent.Right = node.Right;

                    node.Right.Parent = node.Parent;
                }
                else
                {
                    if (parent.Left == node)
                        parent.Left = node.Left;
                    else
                        parent.Right = node.Left;

                    node.Left.Parent = node.Parent;
                }

                return true;
            }

            // III: удаляемый элемент - имеет двух потомков
            if (node.Left != null && node.Right != null)
            {
                var successor = Next(key);
                node.Data = successor.Data;
                if (successor.Parent.Left == successor)
                {
                    successor.Parent.Left = successor.Right;
                    if (successor.Right != null)
                        successor.Right.Parent = successor.Parent;
                }
                else
                {
                    successor.Parent.Right = successor.Left;
                    if (successor.Left != null)
                        successor.Right.Parent = successor.Parent;
                }

                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
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

        public int Count { get; }
        public bool IsReadOnly { get; }

        public bool ContainsKey(TKey key)
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

        private class Node
        {
            public KeyValuePair<TKey, TValue> Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
        }
    }
}