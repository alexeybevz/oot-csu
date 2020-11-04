namespace Homework02
{
    public interface ICostPromo
    {
        CostPromoPriority Priority { get; }
        decimal ApplyDiscount(decimal total);
    }
}