using System.Collections.Generic;

namespace Homework02
{
    public interface IBookVisitor
    {
        void VisitPaperBook(Book book);
        void VisitDigitalBook(Book book);
        decimal GetDeliveryPrice();
        decimal GetTotalCost();
        void ResetVisitor();
    }
}