namespace Homework02
{
    public abstract class BookItem
    {
        public Book Book { get; set; }
        public int Qty { get; set; }

        public abstract void Accept(IBookItemVisitor visitor);
    }
}