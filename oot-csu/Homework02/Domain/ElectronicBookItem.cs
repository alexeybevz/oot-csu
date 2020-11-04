namespace Homework02
{
    public class ElectronicBookItem : BookItem
    {
        public ElectronicBookItem(BookVisitor visitor)
        {
            Visitor = visitor;
        }
    }
}