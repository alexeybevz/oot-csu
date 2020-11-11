using System;
using System.Collections.Generic;

namespace Homework02
{
    public class DiscountCurrency : IPromo
    {
        private readonly decimal _discount;
        public CostPromoPriority Priority { get; }

        public DiscountCurrency(decimal discount, CostPromoPriority priority)
        {
            if (discount <= 0)
                throw new ArgumentException("Размер скидки должен быть положительным числом");

            _discount = discount;
            Priority = priority;
        }

        public void ApplyPromo(ref decimal booksTotalCost, IEnumerable<Book> orderedBooks, ref decimal deliveryCost)
        {
            booksTotalCost -= booksTotalCost - _discount <= 0 ? booksTotalCost : _discount;
        }
    }
}