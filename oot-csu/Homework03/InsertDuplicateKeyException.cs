using System;

namespace Homework03
{
    public class InsertDuplicateKeyException : Exception
    {
        public InsertDuplicateKeyException() { }
        public InsertDuplicateKeyException(string key)
            : base($"Attempt insert to binary search tree a duplicate key: {key}") { }
    }
}