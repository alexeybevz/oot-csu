namespace Homework02
{
    public class ElectronicBookItem : BookItem
    {
        public override void Accept(IBookItemVisitor visitor)
        {
            visitor.VisitElectronicBook(this);
        }
    }
}