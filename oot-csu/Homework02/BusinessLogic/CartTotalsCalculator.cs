using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Promo;

namespace Homework02
{
    public class CartTotalsCalculator : ICartTotalsCalculator
    {
        private readonly ICollection<IPromo> _permanentPromos;
        private decimal _totalCost;
        private decimal _totalCostPaperBooks;

        public CartTotalsCalculator(ICollection<IPromo> permanentPromos = null)
        {
            _permanentPromos = permanentPromos ?? new List<IPromo>();
        }

        public void Add(IPromo promo)
        {
            if (promo != null)
                _permanentPromos.Add(promo);
        }

        public void Remove(IPromo promo)
        {
            if (promo != null)
                _permanentPromos.Remove(promo);
        }

        public void VisitPaperBook(Book book)
        {
            if (book == null)
                return;

            decimal bookCost = GetBookCost(book);
            _totalCost += bookCost;
            _totalCostPaperBooks += bookCost;
        }

        public void VisitDigitalBook(Book book)
        {
            if (book == null)
                return;

            _totalCost += GetBookCost(book);
        }

        public CartTotals GetCartTotals(IEnumerable<Book> orderedBooks, IEnumerable<IPromo> shoppingCartPromos)
        {
            ResetVisitor();
            foreach (var orderedBook in orderedBooks)
                orderedBook.Accept(this);

            var cartTotal = new CartTotals()
            {
                BooksTotalCost = GetTotalCost(),
                OrderedBooks = orderedBooks,
                DeliveryCost = GetDeliveryPrice(),
            };

            var promos = _permanentPromos.Union(shoppingCartPromos);

            foreach (var promo in promos.OrderBy(x => x.Priority))
                promo.ApplyPromo(cartTotal);

            return cartTotal;
        }

        private decimal GetDeliveryPrice()
        {
            return (_totalCostPaperBooks == 0 || _totalCostPaperBooks >= 1000) ? 0 : 200;
        }

        private decimal GetTotalCost()
        {
            return _totalCost;
        }

        private void ResetVisitor()
        {
            _totalCost = 0;
            _totalCostPaperBooks = 0;
        }

        private decimal GetBookCost(Book book)
        {
            return (book.Price * book.Qty);
        }
    }
}