using System.Collections.Generic;
using Homework01.Domain;

namespace Homework01.BusinessLogic
{
    public interface IPersonRelationshipRepository
    {
        List<Person> GetParents(Person person);
        List<Person> GetAuntsAndUncles(Person person);
        List<Person> GetCousins(Person person);
        List<Person> GetPartnerParents(Person person);
    }
}