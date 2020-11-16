using System;
using System.Linq;
using BusinessLogic.Promo;

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

        public void ApplyPromo(CartTotals cartTotals)
        {
            var matchedBookItems = cartTotals.OrderedBooks.Where(b =>
                b.Author == _freeBook.Author &&
                b.Title == _freeBook.Title).ToList();

            if (matchedBookItems.Any())
                cartTotals.BooksTotalCost -= matchedBookItems.Sum(b => b.Price);
        }
    }
}