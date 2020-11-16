using BusinessLogic.Promo;

namespace Homework02
{
    public interface IPromo
    {
        CostPromoPriority Priority { get; }
        void ApplyPromo(CartTotals cartTotals);
    }
}