namespace Homework02
{
    public class BaseBookItemVisitor : IBookItemVisitor
    {
        private decimal _totalCost;
        private decimal _totalCostPaperBookItems;

        public void VisitPaperBook(BookItem bookItem)
        {
            if (bookItem == null)
                return;

            decimal bookItemCost = GetBookItemCost(bookItem);
            _totalCost += bookItemCost;
            _totalCostPaperBookItems += bookItemCost;
        }

        public void VisitElectronicBook(BookItem bookItem)
        {
            if (bookItem == null)
                return;

            _totalCost += GetBookItemCost(bookItem);
        }

        public decimal GetDeliveryPrice()
        {
            return (_totalCostPaperBookItems == 0 || _totalCostPaperBookItems >= 1000) ? 0 : 200;
        }

        public decimal GetTotalCost()
        {
            return _totalCost;
        }

        public void ResetVisitor()
        {
            _totalCost = 0;
            _totalCostPaperBookItems = 0;
        }

        private decimal GetBookItemCost(BookItem bookItem)
        {
            return (bookItem.Book.Price * bookItem.Qty);
        }
    }
}