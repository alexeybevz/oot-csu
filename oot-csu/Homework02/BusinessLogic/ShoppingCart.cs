using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Promo;

namespace Homework02
{
    public class ShoppingCart
    {
        private readonly List<Book> _orderedBooks;
        private readonly ICartTotalsCalculator _cartTotalsCalculator;
        private readonly ICollection<IPromo> _promos;

        public ShoppingCart(ICartTotalsCalculator cartTotalsCalculator, ICollection<IPromo> promos = null)
        {
            if (cartTotalsCalculator == null)
                throw new ArgumentException("BookItemVisitor not found");

            _orderedBooks = new List<Book>();
            _cartTotalsCalculator = cartTotalsCalculator;
            _promos = promos ?? new List<IPromo>();
        }

        public void Add(Book book)
        {
            if (book != null)
                _orderedBooks.Add(book);
        }

        public void Remove(Book book)
        {
            if (book != null)
                _orderedBooks.Remove(book);
        }

        public void Clear()
        {
            _orderedBooks.Clear();
        }

        public decimal GetTotal()
        {
            _cartTotalsCalculator.ResetVisitor();
            _orderedBooks.ForEach(bi => bi.Accept(_cartTotalsCalculator));

            var cartTotal = new CartTotals()
            {
                BooksTotalCost = _cartTotalsCalculator.GetTotalCost(),
                OrderedBooks = _orderedBooks,
                DeliveryCost = _cartTotalsCalculator.GetDeliveryPrice(),
            };

            foreach (var promo in _promos.OrderBy(x => x.Priority))
                promo.ApplyPromo(cartTotal);

            return cartTotal.BooksTotalCost + cartTotal.DeliveryCost;
        }

        public List<Book> GetOrderedBooks()
        {
            return _orderedBooks;
        }
    }
}