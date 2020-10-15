using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Homework01.Domain
{
    public class Person
    {
        private List<Person> childs;

        public string Name { get; }
        public Gender Gender { get; }
        public Person Mother { get; private set; }
        public Person Father { get; private set; }
        public Person Partner { get; private set; }
        public ReadOnlyCollection<Person> Childs => childs.AsReadOnly();

        public Person(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
            childs = new List<Person>();
        }

        public void CreateRelationshipWith(Person person)
        {
            ValidPerson(person);

            if (Partner != null)
                Partner.Partner = null;

            Partner = person;
            person.Partner = this;
        }
        
        public void SetMother(Person mother)
        {
            ValidPerson(mother);

            if (mother == Father)
                throw new ArgumentException("Родителями должны быть разные люди");

            Mother?.RemoveChild(this);

            Mother = mother;
            mother.AddChild(this);
        }

        public void SetFather(Person father)
        {
            ValidPerson(father);

            if (father == Mother)
                throw new ArgumentException("Родителями должны быть разные люди");

            Father?.RemoveChild(this);

            Father = father;
            father.AddChild(this);
        }

        public void AddChild(Person child)
        {
            ValidPerson(child);

            if (child.Mother != this && child.Father != this)
                throw new ArgumentException("Нельзя добавить ребенка тому, кто не является его родителем");

            childs.Add(child);
        }

        public void RemoveChild(Person child)
        {
            if (child == null)
                throw new ArgumentException("Передан пустой объект при выполнении действия");

            childs.Remove(child);
        }

        private void ValidPerson(Person person)
        {
            if (person == null)
                throw new ArgumentException("Передан пустой объект при выполнении действия");

            if (this == person)
                throw new ArgumentException("Нельзя выполнить это действие для самого себя");
        }
    }
}