using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework02
{
    public class FreeBookPromo : IPromo
    {
        private readonly Book _freeBook;

        public CostPromoPriority Priority { get; }

        public FreeBookPromo(Book freeBook, CostPromoPriority priority)
        {
            if (freeBook == null)
                throw new ArgumentException("Необходимо указать книгу, которая участвует в промо");

            _freeBook = freeBook;
            Priority = priority;
        }

        public void ApplyPromo(ref decimal booksTotalCost, ICollection<BookItem> bookItems, ref decimal deliveryCost)
        {
            var matchedBookItems = bookItems.Where(b =>
                b.Book.Author == _freeBook.Author &&
                b.Book.Title == _freeBook.Title).ToList();

            booksTotalCost -=
                matchedBookItems.Any() ?
                matchedBookItems.Sum(b => b.Book.Price)
                : 0;
        }
    }
}