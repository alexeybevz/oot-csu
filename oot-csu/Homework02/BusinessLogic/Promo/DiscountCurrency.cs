using System;

namespace Homework02
{
    public class DiscountCurrency : ICostPromo
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

        public decimal ApplyDiscount(decimal total)
        {
            var result = total - _discount;
            return result <= 0 ? 0 : result;
        }
    }
}