namespace Homework02
{
    public class PaperBook : Book
    {
        public BookCover Cover { get; set; }

        public override void Accept(ICartTotalsCalculator cartTotalsCalculator)
        {
            cartTotalsCalculator.VisitPaperBook(this);
        }
    }
}