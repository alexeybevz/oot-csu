using System;

namespace Homework02
{
    public class FreeBookPromo : IBookPromo
    {
        private readonly Book _freeBook;

        public FreeBookPromo(Book freeBook)
        {
            if (freeBook == null)
                throw new ArgumentException("Необходимо указать книгу, которая участвует в промо");

            _freeBook = freeBook;
        }

        public decimal ApplyPromo(BookItem book)
        {
            if (book == null)
                throw new ArgumentException("Не указана книга для применения промокода");

            bool isMatch = _freeBook.Author == book.Book.Author && _freeBook.Title == book.Book.Title;
            return isMatch ? book.Book.Price : 0;
        }
    }
}