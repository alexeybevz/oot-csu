using System.Collections.Generic;
using System.Linq;

namespace Homework02
{
    public class ShoppingCart
    {
        private readonly List<BookItem> _bookItems;
        private readonly List<BookVisitor> _bookVisitors;
        private readonly ICollection<IPromo> _promos;

        public ShoppingCart(List<BookVisitor> bookVisitors, ICollection<IPromo> promos = null)
        {
            _bookItems = new List<BookItem>();
            _bookVisitors = bookVisitors ?? new List<BookVisitor>();
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
            decimal totalBooksCost = 0;
            decimal deliveryCost = 0;

            _bookVisitors?.ForEach(dv => dv.ResetVisitor());
            _bookItems.ForEach(bi => bi.Visit());

            _bookVisitors?.ForEach(dv =>
            {
                totalBooksCost += dv.GetTotalCost();
                deliveryCost += dv.GetDeliveryPrice();
            });

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