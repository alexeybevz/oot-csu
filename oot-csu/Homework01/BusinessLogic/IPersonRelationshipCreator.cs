using Homework01.Domain;

namespace Homework01.BusinessLogic
{
    public interface IPersonRelationshipCreator
    {
        void AddParent(Person person, Person parent1, Person parent2);
        void AddMarriedCouple(Person person, Person partner);
    }
}