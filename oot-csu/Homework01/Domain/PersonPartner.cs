namespace Homework01.Domain
{
    internal class PersonPartner
    {
        public Person Person { get; }
        public Person Partner { get; }

        public PersonPartner(Person person, Person partner)
        {
            Person = person;
            Partner = partner;
        }
    }
}