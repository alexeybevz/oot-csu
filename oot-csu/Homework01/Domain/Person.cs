namespace Homework01.Domain
{
    public class Person
    {
        public string Name { get; }
        public Gender Gender { get; }

        public Person(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
        }
    }
}