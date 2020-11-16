using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Promo;

namespace Homework02
{
    public class ShoppingCart
    {
        private readonly List<Book> _orderedBooks;
        private readonly IBookVisitor _bookVisitor;
        private readonly ICollection<IPromo> _promos;

        public ShoppingCart(IBookVisitor bookVisitor, ICollection<IPromo> promos = null)
        {
            if (bookVisitor == null)
                throw new ArgumentException("BookItemVisitor not found");

            _orderedBooks = new List<Book>();
            _bookVisitor = bookVisitor;
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
            _bookVisitor.ResetVisitor();
            _orderedBooks.ForEach(bi => bi.Accept(_bookVisitor));

            var cartTotal = new CartTotals()
            {
                BooksTotalCost = _bookVisitor.GetTotalCost(),
                OrderedBooks = _orderedBooks,
                DeliveryCost = _bookVisitor.GetDeliveryPrice(),
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