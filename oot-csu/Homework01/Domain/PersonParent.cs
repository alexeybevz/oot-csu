namespace Homework01.Domain
{
    internal class PersonParent
    {
        public Person Child { get; }
        public Person Parent { get; }

        public PersonParent(Person child, Person parent)
        {
            Child = child;
            Parent = parent;
        }
    }
}