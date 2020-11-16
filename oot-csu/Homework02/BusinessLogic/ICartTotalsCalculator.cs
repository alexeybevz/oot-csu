using System.Collections.Generic;
using BusinessLogic.Promo;

namespace Homework02
{
    public interface ICartTotalsCalculator
    {
        void VisitPaperBook(Book book);
        void VisitDigitalBook(Book book);
        CartTotals GetCartTotals(IEnumerable<Book> orderedBooks);
    }
}