namespace Homework02
{
    public class DigitalBook : Book
    {
        public DigitalBookFormat Format { get; set; }

        public override void Accept(IBookVisitor visitor)
        {
            visitor.VisitDigitalBook(this);
        }
    }
}