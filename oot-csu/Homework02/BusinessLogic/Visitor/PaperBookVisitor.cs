namespace Homework02
{
    public class PaperBookVisitor: BookVisitor
    {
        private decimal totalCost;

        public override void VisitBookItem(BookItem bookItem)
        {
            totalCost += GetBookItemCost(bookItem);
        }

        public override decimal GetDeliveryPrice()
        {
            return (totalCost == 0 || totalCost >= 1000) ? 0 : 200;
        }

        public override decimal GetTotalCost()
        {
            return totalCost;
        }

        public override void ResetVisitor()
        {
            totalCost = 0;
        }
    }
}