using System;
using System.Collections.Generic;

namespace Homework02
{
    public class DiscountPercent : IPromo
    {
        private readonly decimal _discountPercent;
        public CostPromoPriority Priority { get; }

        public DiscountPercent(decimal discountPercent, CostPromoPriority priority)
        {
            if (discountPercent <= 0 || discountPercent > 100)
                throw new ArgumentException("Размер скидки должен быть больше 0 и меньше либо равен 100");

            _discountPercent = discountPercent;
            Priority = priority;
        }

        public void ApplyPromo(ref decimal booksTotalCost, ICollection<Book> orderedBooks, ref decimal deliveryCost)
        {
            booksTotalCost -=
                booksTotalCost - booksTotalCost * (_discountPercent / 100) <= 0
                ? booksTotalCost
                : booksTotalCost * (_discountPercent / 100);
        }
    }
}