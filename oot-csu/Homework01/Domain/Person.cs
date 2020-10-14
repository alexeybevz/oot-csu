using System;
using System.Collections.Generic;

namespace Homework01.Domain
{
    public class Person
    {
        public string Name { get; }
        public Gender Gender { get; }
        public Person Mother { get; private set; }
        public Person Father { get; private set; }
        public Person Partner { get; private set; }
        public List<Person> Childs { get; }

        public Person(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
            Childs = new List<Person>();
        }

        public void SetPartner(Person person)
        {
            ValidPerson(person);
            Partner = person;
        }

        private void ValidPerson(Person person)
        {
            if (person == null)
                throw new ArgumentException("Передан пустой объект при выполнении действия");

            if (this == person)
                throw new ArgumentException("Нельзя выполнить это действие для самого себя");
        }

        public void SetMother(Person mother)
        {
            ValidPerson(mother);

            if (mother == Father)
                throw new ArgumentException("Родителями должны быть разные люди");

            Mother = mother;
            mother.Childs.Add(this);
        }

        public void SetFather(Person father)
        {
            ValidPerson(father);

            if (father == Mother)
                throw new ArgumentException("Родителями должны быть разные люди");

            Father = father;
            father.Childs.Add(this);
        }
    }
}