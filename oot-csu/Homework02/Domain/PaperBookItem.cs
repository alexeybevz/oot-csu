namespace Homework02
{
    public class PaperBookItem : BookItem
    {
        public PaperBookItem(BookVisitor visitor)
        {
            Visitor = visitor;
        }
    }
}