using System;
using System.Linq;
using Homework02;

namespace BusinessLogic.Promo
{
    public class ExtraFreeBooksPromo : IPromo
    {
        private readonly IBookRepository _booksRepository;

        public ExtraFreeBooksPromo(IBookRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public CostPromoPriority Priority { get; }

        public void ApplyPromo(CartTotals cartTotals)
        {
            var statsByAuthors = cartTotals.OrderedBooks
                .OfType<PaperBook>()
                .GroupBy(book => book.Author)
                .Where(g => g.Count() > 1)
                .ToDictionary(group => group.Key, group => Math.Floor((decimal)(group.Count() / 2)));

            foreach (var row in statsByAuthors)
            {
                var targetAuthor = row.Key;
                var countNeedExtraBooks = row.Value;

                var orderedDigitalBookTitles =
                    cartTotals.OrderedBooks
                        .OfType<DigitalBook>()
                        .Where(x => x.Author == targetAuthor)
                        .Select(x => x.Title)
                        .ToList();

                var availableBooks = _booksRepository
                    .GetBooks()
                    .OfType<DigitalBook>()
                    .Where(b => b.Author == targetAuthor)
                    .Where(b => !orderedDigitalBookTitles.Contains(b.Title))
                    .ToList();

                var extraFreeBooks = availableBooks.Take((int)countNeedExtraBooks);
                cartTotals.ExtraFreeBooks.AddRange(extraFreeBooks);
            }
        }
    }
}