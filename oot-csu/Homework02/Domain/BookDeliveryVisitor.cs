namespace Homework02
{
    public abstract  class BookVisitor
    {
        public abstract void VisitBookItem(BookItem bookItem);
        public abstract decimal GetDeliveryPrice();
        public abstract decimal GetTotalCost();
        public abstract void ResetVisitor();

        protected decimal GetBookItemCost(BookItem bookItem)
        {
            return (bookItem.Book.Price * bookItem.Qty);
        }
    }
}