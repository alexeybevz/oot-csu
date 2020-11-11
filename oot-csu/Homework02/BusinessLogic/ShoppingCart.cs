using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework02
{
    public class ShoppingCart
    {
        private readonly List<BookItem> _bookItems;
        private readonly IBookItemVisitor _bookItemVisitor;
        private readonly ICollection<IPromo> _promos;

        public ShoppingCart(IBookItemVisitor bookItemVisitor, ICollection<IPromo> promos = null)
        {
            if (bookItemVisitor == null)
                throw new ArgumentException("BookItemVisitor not found");

            _bookItems = new List<BookItem>();
            _bookItemVisitor = bookItemVisitor;
            _promos = promos ?? new List<IPromo>();
        }

        public void Add(BookItem bookItem)
        {
            if (bookItem != null)
                _bookItems.Add(bookItem);
        }

        public void Remove(BookItem bookItem)
        {
            if (bookItem != null)
                _bookItems.Remove(bookItem);
        }

        public void Clear()
        {
            _bookItems.Clear();
        }

        public decimal GetTotal()
        {
            _bookItemVisitor.ResetVisitor();
            _bookItems.ForEach(bi => bi.Accept(_bookItemVisitor));

            decimal totalBooksCost = _bookItemVisitor.GetTotalCost();
            decimal deliveryCost = _bookItemVisitor.GetDeliveryPrice();

            foreach (var promo in _promos.OrderBy(x => x.Priority))
                promo.ApplyPromo(ref totalBooksCost, _bookItems, ref deliveryCost);

            return totalBooksCost + deliveryCost;
        }

        public List<BookItem> GetOrderedBooks()
        {
            return _bookItems;
        }
    }
}