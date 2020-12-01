﻿using System.Collections.Generic;
using Xunit;

namespace Homework03
{
    public class UnitTests
    {
        /* Тестируемое бинарное дерево поиска
                8
               / \
              3   10
             / \    \
            1   6    14
               / \   /
              4   7 13
         */

        readonly BinarySearchTree<int, int> tree =
            new BinarySearchTree<int, int>
        {
            {8, 8},
            {3, 3},
            {10, 10},
            {1, 1},
            {6, 6},
            {14, 14},
            {4, 4},
            {7, 7},
            {13, 13},
        };

        private List<int> GetActual()
        {
            var actual = new List<int>();
            var enumerator = tree.GetEnumerator();
            while (enumerator.MoveNext())
                actual.Add(enumerator.Current.Key);
            return actual;
        }

        [Fact]
        public void PreOrderTraversal()
        {
            var expected = new List<int>() {8, 3, 1, 6, 4, 7, 10, 14, 13};
            var actual = GetActual();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveOneLeaf()
        {
            tree.Remove(13);

            var expected = new List<int>() { 8, 3, 1, 6, 4, 7, 10, 14 };
            var actual = GetActual();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveNodeWithOneSubNode()
        {
            tree.Remove(14);

            var expected = new List<int>() { 8, 3, 1, 6, 4, 7, 10, 13 };
            var actual = GetActual();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveNodeWithTwoSubNode()
        {
            tree.Remove(3);

            var expected = new List<int>() { 8, 4, 1, 6, 7, 10, 14, 13 };
            var actual = GetActual();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveRootNode()
        {
            tree.Remove(8);

            var expected = new List<int>() { 10, 3, 1, 6, 4, 7, 14, 13 };
            var actual = GetActual();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryGetKeys()
        {
            var treeTest = new BinarySearchTree<int, int>()
            {
                {1, 2},
                {3, 4},
            };

            Assert.Equal(new List<int>() { 1, 3 }, treeTest.Keys);
        }

        [Fact]
        public void TryGetValues()
        {
            var treeTest = new BinarySearchTree<int, int>()
            {
                {1, 2},
                {3, 4},
            };

            Assert.Equal(new List<int>() { 2, 4 }, treeTest.Values);
        }

        [Fact]
        public void TryClearTree()
        {
            var treeTest = new BinarySearchTree<int, int>()
            {
                {1, 2},
                {3, 4},
            };

            treeTest.Clear();
            Assert.Equal(0, treeTest.Keys.Count);
        }

        [Fact]
        public void TryAddExistsKeyInTree()
        {
            var treeTest = new BinarySearchTree<int, int>() { {1, 1} };
            Assert.Throws<InsertDuplicateKeyException>(() => treeTest.Add(1, 2));
            Assert.True(treeTest.Keys.Count == 1);
        }

        [Fact]
        public void TryContainsKeyIsTrue()
        {
            Assert.True(tree.ContainsKey(7));
        }

        [Fact]
        public void TryContainsKeyIsFalse()
        {
            Assert.False(tree.ContainsKey(999));
        }

        [Fact]
        public void TryContainsKeyValueIsTrue()
        {
            Assert.True(tree.Contains(new KeyValuePair<int, int>(7, 7)));
        }

        [Fact]
        public void TryContainsKeyValueIsFalse()
        {
            Assert.False(tree.Contains(new KeyValuePair<int, int>(7, 777)));
        }
    }
}