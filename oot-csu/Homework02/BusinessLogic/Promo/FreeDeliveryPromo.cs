using BusinessLogic.Promo;

namespace Homework02
{
    public class FreeDeliveryPromo : IPromo
    {
        public CostPromoPriority Priority { get; }

        public FreeDeliveryPromo(CostPromoPriority priority)
        {
            Priority = priority;
        }

        public void ApplyPromo(CartTotals cartTotals)
        {
            cartTotals.DeliveryCost -= cartTotals.DeliveryCost;
        }
    }
}