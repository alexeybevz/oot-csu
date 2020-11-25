using System.Collections.Generic;
using Xunit;

namespace Homework03
{
    public class UnitTests
    {
        /* Тестируемое бинарное дерево поиска
                8
               / \
              3   10
             / \   \
            1   6   14
               / \   \
              4   7   13
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

        [Fact]
        public void PreOrderTraversal()
        {
            var expected = new List<int>() {8, 3, 1, 6, 4, 7, 10, 14, 13};
            var actual = new List<int>();
            var enumerator = tree.GetEnumerator();
            while (enumerator.MoveNext())
                actual.Add(enumerator.Current.Key);

            Assert.Equal(expected, actual);
        }
    }
}