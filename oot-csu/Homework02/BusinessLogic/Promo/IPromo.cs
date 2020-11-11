using System.Collections.Generic;

namespace Homework02
{
    public interface IPromo
    {
        CostPromoPriority Priority { get; }
        void ApplyPromo(ref decimal booksTotalCost, IEnumerable<Book> orderedBooks, ref decimal deliveryCost);
    }
}