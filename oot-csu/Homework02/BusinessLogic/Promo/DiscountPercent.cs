using System;

namespace Homework02
{
    public class DiscountPercent : ICostPromo
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

        public decimal ApplyDiscount(decimal total)
        {
            var result = total - total * (_discountPercent / 100);
            return result <= 0 ? 0 : result;
        }
    }
}