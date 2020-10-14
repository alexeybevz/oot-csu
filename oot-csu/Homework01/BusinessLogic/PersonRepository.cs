using System.Collections.Generic;
using System.Linq;
using Homework01.Domain;

namespace Homework01.BusinessLogic
{
    public class PersonRepository : IPersonRepository
    {
        public List<Person> GetParents(Person person)
        {
            var parents = new List<Person>();

            if (person?.Mother != null)
                parents.Add(person.Mother);

            if (person?.Father != null)
                parents.Add(person.Father);

            return parents;
        }

        public List<Person> GetAuntsAndUncles(Person person)
        {
            if (person == null)
                return new List<Person>();

            var personParents = GetParents(person);

            var grandParents = new List<Person>();
            personParents.ForEach(x => grandParents.AddRange(GetParents(x)));

            return grandParents
                .SelectMany(x => x.Childs)
                .Except(personParents)
                .ToList();
        }

        public List<Person> GetCousins(Person person)
        {
            return person == null
                ? new List<Person>()
                : GetAuntsAndUncles(person).SelectMany(x => x.Childs).ToList();
        }

        public List<Person> GetPartnerParents(Person person)
        {
            return person?.Partner == null
                ? new List<Person>()
                : GetParents(person.Partner);
        }
    }
}