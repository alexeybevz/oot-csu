namespace Homework02
{
    public interface ICartTotalsCalculator
    {
        void VisitPaperBook(Book book);
        void VisitDigitalBook(Book book);
        decimal GetDeliveryPrice();
        decimal GetTotalCost();
        void ResetVisitor();
    }
}