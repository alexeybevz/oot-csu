using System;
using BusinessLogic.Promo;

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

        public void ApplyPromo(CartTotals cartTotals) =>
            cartTotals.BooksTotalCost *= (100 - _discountPercent) / 100;
    }
}