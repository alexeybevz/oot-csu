using System;
using System.Collections.Generic;
using System.Linq;
using Homework01.Domain;

namespace Homework01.BusinessLogic
{
    public class PersonRelationshipRepository : IPersonRelationshipCreator, IPersonRelationshipRepository
    {
        private readonly List<PersonParent> parents;
        private readonly List<PersonPartner> marriedCouples;

        public PersonRelationshipRepository()
        {
            parents = new List<PersonParent>();
            marriedCouples = new List<PersonPartner>();
        }

        public void AddParent(Person person, Person parent1, Person parent2)
        {
            if (person == parent1 || person == parent2)
                throw new ArgumentException("Нельзя быть родителем самому себе");

            if (parent1.Gender == parent2.Gender)
                throw new ArgumentException("Нельзя добавить родителей одного пола");

            if (parent1 == parent2)
                throw new ArgumentException("Родителями должны являться разные люди");

            if (!marriedCouples.Any(x =>
                    (x.Person == parent1 && x.Partner == parent2) ||
                    (x.Person == parent2 && x.Partner == parent1)))
                throw new ArgumentException("Родителями могут стать только супруги");

            parents.Add(new PersonParent(person, parent1));
            parents.Add(new PersonParent(person, parent2));
        }

        public void AddMarriedCouple(Person person, Person partner)
        {
            if (marriedCouples.Any(x => x.Person == person || x.Partner == person))
                throw new ArgumentException($"{person.Name} уже имеет партнера");

            if (marriedCouples.Any(x => x.Person == partner || x.Partner == partner))
                throw new ArgumentException($"{partner.Name} уже имеет партнера");

            marriedCouples.Add(new PersonPartner(person, partner));
        }

        public List<Person> GetParents(Person person)
        {
            return parents
                .Where(x => x.Child == person)
                .Select(x => x.Parent).ToList();
        }

        public List<Person> GetAuntsAndUncles(Person person)
        {
            var personParents = GetParents(person);

            var grandParents = new List<Person>();
            personParents.ForEach(x => grandParents.AddRange(GetParents(x)));

            var childsOfGrandParents = parents
                .Where(x => grandParents.Contains(x.Parent) && !personParents.Contains(x.Child))
                .Select(x => x.Child)
                .ToList();

            return childsOfGrandParents
                .GroupBy(x => x)
                .Select(x => x.First())
                .ToList();
        }

        public List<Person> GetCousins(Person person)
        {
            var childsOfGrandParents = GetAuntsAndUncles(person);

            var childs = parents
                .Where(x => childsOfGrandParents.Contains(x.Parent))
                .Select(x => x.Child)
                .ToList();
            return childs;
        }

        public List<Person> GetPartnerParents(Person person)
        {
            var parther = marriedCouples
                .Where(x => x.Person == person || x.Partner == person)
                .Select(x => x.Person == person ? x.Partner : x.Person).FirstOrDefault();

            return parther == null ? new List<Person>() : GetParents(parther);
        }
    }
}