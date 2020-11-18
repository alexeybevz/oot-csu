using System;
using System.Collections.Generic;

namespace Homework02
{
    public class ShoppingCart
    {
        private readonly List<Book> _orderedBooks;
        private readonly ICartTotalsCalculator _cartTotalsCalculator;
        private readonly List<IPromo> _promos;

        public ShoppingCart(ICartTotalsCalculator cartTotalsCalculator)
        {
            if (cartTotalsCalculator == null)
                throw new ArgumentException("CartTotalsCalculator not found");

            _orderedBooks = new List<Book>();
            _cartTotalsCalculator = cartTotalsCalculator;
            _promos = new List<IPromo>();
        }

        public void Add(Book book)
        {
            if (book != null)
                _orderedBooks.Add(book);
        }

        public void Add(IPromo promo)
        {
            if (promo != null)
                _promos.Add(promo);
        }

        public void AddRange(List<Book> books)
        {
            if (books != null)
                _orderedBooks.AddRange(books);
        }

        public void AddRange(List<IPromo> promos)
        {
            if (promos != null)
                _promos.AddRange(promos);
        }

        public void Remove(Book book)
        {
            if (book != null)
                _orderedBooks.Remove(book);
        }

        public void Remove(IPromo promo)
        {
            if (promo != null)
                _promos.Remove(promo);
        }

        public void Clear()
        {
            _orderedBooks.Clear();
        }

        public decimal GetTotal()
        {
            var cartTotal = _cartTotalsCalculator.GetCartTotals(_orderedBooks, _promos);
            return cartTotal.BooksTotalCost + cartTotal.DeliveryCost;
        }

        public List<Book> GetOrderedBooks()
        {
            return _orderedBooks;
        }

        public List<Book> GetExtraFreeBooks()
        {
            var cartTotal = _cartTotalsCalculator.GetCartTotals(_orderedBooks, _promos);
            return cartTotal.ExtraFreeBooks;
        }
    }
}