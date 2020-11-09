using System.Collections.Generic;

namespace Homework02
{
    public class FreeDeliveryPromo : IPromo
    {
        public CostPromoPriority Priority { get; }

        public FreeDeliveryPromo(CostPromoPriority priority)
        {
            Priority = priority;
        }

        public void ApplyPromo(ref decimal booksTotalCost, ICollection<BookItem> bookItems, ref decimal deliveryCost)
        {
            deliveryCost -= deliveryCost;
        }
    }
}