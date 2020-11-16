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

        public ShoppingCart(ICartTotalsCalculator cartTotalsCalculator)
        {
            if (cartTotalsCalculator == null)
                throw new ArgumentException("BookItemVisitor not found");

            _orderedBooks = new List<Book>();
            _cartTotalsCalculator = cartTotalsCalculator;
        }

        public void Add(Book book)
        {
            if (book != null)
                _orderedBooks.Add(book);
        }

        public void AddRange(List<Book> books)
        {
            if (books != null)
                _orderedBooks.AddRange(books);
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
            var cartTotal = _cartTotalsCalculator.GetCartTotals(_orderedBooks);
            return cartTotal.BooksTotalCost + cartTotal.DeliveryCost;
        }

        public List<Book> GetOrderedBooks()
        {
            return _orderedBooks;
        }

        public List<Book> GetExtraFreeBooks()
        {
            var cartTotal = _cartTotalsCalculator.GetCartTotals(_orderedBooks);
            return cartTotal.ExtraFreeBooks;
        }
    }
}