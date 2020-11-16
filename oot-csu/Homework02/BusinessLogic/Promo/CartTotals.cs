using System;
using System.Collections.Generic;
using Homework02;

namespace BusinessLogic.Promo
{
    public class CartTotals
    {
        private decimal _bookTotalCost;
        private decimal _deliveryCost;

        public IEnumerable<Book> OrderedBooks { get; set; }

        public decimal BooksTotalCost
        {
            get { return _bookTotalCost; }
            set { _bookTotalCost = Math.Max(value, 0); }
        }

        public decimal DeliveryCost
        {
            get { return _deliveryCost; }
            set { _deliveryCost = Math.Max(value, 0); }
        }
    }
}