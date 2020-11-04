namespace Homework02
{
    public class ElectronicBookVisitor : BookVisitor
    {
        private decimal totalCost;

        public override void VisitBookItem(BookItem bookItem)
        {
            totalCost += GetBookItemCost(bookItem);
        }

        public override decimal GetDeliveryPrice()
        {
            return 0;
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