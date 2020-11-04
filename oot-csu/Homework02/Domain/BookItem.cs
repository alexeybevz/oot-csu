namespace Homework02
{
    public abstract class BookItem
    {
        public BookVisitor Visitor { get; protected set; }
        public Book Book { get; set; }
        public int Qty { get; set; }

        public void Visit()
        {
            Visitor.VisitBookItem(this);
        }
    }
}