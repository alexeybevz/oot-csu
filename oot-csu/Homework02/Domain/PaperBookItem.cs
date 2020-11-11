namespace Homework02
{
    public class PaperBookItem : BookItem
    {
        public override void Accept(IBookItemVisitor visitor)
        {
            visitor.VisitPaperBook(this);
        }
    }
}