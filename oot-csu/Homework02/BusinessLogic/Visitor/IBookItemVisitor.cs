using System.Collections.Generic;

namespace Homework02
{
    public interface IBookItemVisitor
    {
        void VisitPaperBook(BookItem bookItem);
        void VisitElectronicBook(BookItem bookItem);
        decimal GetDeliveryPrice();
        decimal GetTotalCost();
        void ResetVisitor();
    }
}