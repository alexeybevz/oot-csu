using System.Collections.Generic;
using Homework02;

namespace BusinessLogic
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
    }
}