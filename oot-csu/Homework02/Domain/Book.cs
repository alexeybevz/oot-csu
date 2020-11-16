namespace Homework02
{
    public abstract class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; } = 1;
        public abstract void Accept(ICartTotalsCalculator cartTotalsCalculator);
    }
}