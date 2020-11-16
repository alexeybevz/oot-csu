namespace Homework02
{
    public class CartTotalsCalculator : ICartTotalsCalculator
    {
        private decimal _totalCost;
        private decimal _totalCostPaperBooks;

        public void VisitPaperBook(Book book)
        {
            if (book == null)
                return;

            decimal bookCost = GetBookCost(book);
            _totalCost += bookCost;
            _totalCostPaperBooks += bookCost;
        }

        public void VisitDigitalBook(Book book)
        {
            if (book == null)
                return;

            _totalCost += GetBookCost(book);
        }

        public decimal GetDeliveryPrice()
        {
            return (_totalCostPaperBooks == 0 || _totalCostPaperBooks >= 1000) ? 0 : 200;
        }

        public decimal GetTotalCost()
        {
            return _totalCost;
        }

        public void ResetVisitor()
        {
            _totalCost = 0;
            _totalCostPaperBooks = 0;
        }

        private decimal GetBookCost(Book book)
        {
            return (book.Price * book.Qty);
        }
    }
}