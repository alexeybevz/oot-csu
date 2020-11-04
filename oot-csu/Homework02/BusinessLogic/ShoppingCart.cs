using System.Collections.Generic;
using System.Linq;

namespace Homework02
{
    public class ShoppingCart
    {
        private readonly List<BookItem> _bookItems;
        private readonly List<BookVisitor> _bookVisitors;
        private readonly List<ICostPromo> _costPromos;
        private readonly List<IBookPromo> _bookPromos;
        private readonly IDeliveryPromo _deliveryPromo;

        public ShoppingCart(List<BookVisitor> bookVisitors)
        {
            _bookItems = new List<BookItem>();
            _bookVisitors = bookVisitors;
        }

        public ShoppingCart(List<BookVisitor> bookVisitors, List<ICostPromo> costPromos)
        {
            _bookItems = new List<BookItem>();
            _bookVisitors = bookVisitors;
            _costPromos = costPromos.OrderBy(p => p.Priority).ToList();
        }

        public ShoppingCart(List<BookVisitor> bookVisitors, List<ICostPromo> costPromos, List<IBookPromo> bookPromos, IDeliveryPromo deliveryPromo)
        {
            _bookItems = new List<BookItem>();
            _bookVisitors = bookVisitors;
            _costPromos = costPromos.OrderBy(p => p.Priority).ToList();
            _bookPromos = bookPromos;
            _deliveryPromo = deliveryPromo;
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
            decimal totalCost = 0;
            decimal deliveryPrice = 0;

            _bookVisitors?.ForEach(dv => dv.ResetVisitor());
            _bookItems.ForEach(bi => bi.Visit());

            _bookVisitors?.ForEach(dv =>
            {
                totalCost += dv.GetTotalCost();
                deliveryPrice += dv.GetDeliveryPrice();
            });

            _bookPromos?.ForEach(bp =>
                _bookItems.ForEach(bi => totalCost -= bp.ApplyPromo(bi)));

            _costPromos?.ForEach(promo => totalCost = promo.ApplyDiscount(totalCost));

            if (_deliveryPromo != null)
                deliveryPrice = _deliveryPromo.ApplyPromo(deliveryPrice);

            return totalCost + deliveryPrice;
        }

        public List<BookItem> GetOrderedBooks()
        {
            return _bookItems;
        }
    }
}