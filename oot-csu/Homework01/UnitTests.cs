using System;
using System.Collections.Generic;
using System.Linq;
using Homework01.BusinessLogic;
using Homework01.Domain;
using Xunit;

namespace Homework01
{
    public class UnitTests
    {
        static readonly Person _mother = new Person("мама", Gender.Female);
        static readonly Person _father = new Person("папа", Gender.Male);
        static readonly Person _me = new Person("Я", Gender.Male);
        static readonly PersonRepository _repository = new FakePersonRepository().Create(_me, _mother, _father);
        
        [Fact]
        public void GetParentSuccessTest()
        {
            var result = _repository.GetParents(_me).Select(x => x.Name).ToList();
            var expected = new List<string>()
            {
                "мама",
                "папа",
            };
            Assert.Equal(ConcatList(expected), ConcatList(result));
        }

        [Fact]
        public void GetAuntsAndUnclesSuccessTest()
        {
            var result = _repository.GetAuntsAndUncles(_me).Select(x => x.Name).ToList();
            var expected = new List<string>()
            {
                "сестра мамы",
                "брат папы",
            };
            Assert.Equal(ConcatList(expected), ConcatList(result));
        }

        [Fact]
        public void GetUnclesSuccessTest()
        {
            var result = _repository
                .GetAuntsAndUncles(_me)
                .Where(x => x.Gender == Gender.Male)
                .Select(x => x.Name)
                .ToList();
            var expected = new List<string>() { "брат папы" };
            Assert.Equal(ConcatList(expected), ConcatList(result));
        }

        [Fact]
        public void GetCousinsSuccessTest()
        {
            var cousins = _repository.GetCousins(_me).Select(x => x.Name).ToList();
            var expected = new List<string>()
            {
                "дочь сестры мамы",
                "сын сестры мамы",
                "дочь брата папы",
            };
            Assert.Equal(ConcatList(expected), ConcatList(cousins));
        }

        [Fact]
        public void GetOnlyFemaleCousinsSuccessTest()
        {
            var cousins = _repository
                .GetCousins(_me)
                .Where(x => x.Gender == Gender.Female)
                .Select(x => x.Name)
                .ToList();

            var expected = new List<string>()
            {
                "дочь сестры мамы",
                "дочь брата папы",
            };
            Assert.Equal(ConcatList(expected), ConcatList(cousins));
        }

        [Fact]
        public void GetPartnerParentsSuccessTest()
        {
            var partnerParents = _repository.GetPartnerParents(_mother).Select(x => x.Name).ToList();
            var expected = new List<string>()
            {
                "бабушка папы",
                "дедушка папы",
            };
            Assert.Equal(ConcatList(expected), ConcatList(partnerParents));
        }

        [Fact]
        public void CheckConsistencyWhenAddPartnerTest()
        {
            var women = new Person("женщина", Gender.Female);

            var men = new Person("мужчина", Gender.Male);
            men.CreateRelationshipWith(women);

            Assert.Equal(men, women.Partner);
        }

        [Fact]
        public void CheckConsistencyWhenPartnerChangedTest()
        {
            var women1 = new Person("женщина1", Gender.Female);
            var women2 = new Person("женщина2", Gender.Female);

            var men = new Person("мужчина", Gender.Male);
            men.CreateRelationshipWith(women1);
            men.CreateRelationshipWith(women2);

            Assert.Equal(null, women1.Partner);
        }

        [Fact]
        public void CheckConsistencyWhenOtherPartnerReferToFirstTest()
        {
            var women1 = new Person("женщина1", Gender.Female);
            var women2 = new Person("женщина2", Gender.Female);
            var men = new Person("мужчина", Gender.Male);

            men.CreateRelationshipWith(women1);
            women2.CreateRelationshipWith(men);

            Assert.True(women1.Partner == null);
        }

        [Fact]
        public void CheckConsistencyWhenMotherChangedTest()
        {
            var mother1 = new Person("мама1", Gender.Female);
            var mother2 = new Person("мама2", Gender.Female);
            var child = new Person("ребенок", Gender.Male);

            child.SetMother(mother1);
            child.SetMother(mother2);

            Assert.True(child.Mother == mother2 &&
                        mother1.Childs.Count == 0 &&
                        mother2.Childs.Count == 1);
        }

        [Fact]
        public void CheckConsistencyWhenFatherChangedTest()
        {
            var father1 = new Person("папа1", Gender.Male);
            var father2 = new Person("папа2", Gender.Male);
            var child = new Person("ребенок", Gender.Male);

            child.SetMother(father1);
            child.SetMother(father2);

            Assert.True(child.Mother == father2 &&
                        father1.Childs.Count == 0 &&
                        father2.Childs.Count == 1);
        }

        [Fact]
        public void DenyAddChildWithoutParentSuccessTest()
        {
            var child = new Person("ребенок", Gender.Male);
            var father1 = new Person("папа", Gender.Male);

            Assert.Throws<ArgumentException>(() => father1.AddChild(child));
        }

        [Fact]
        public void RemoveChildFromParentTest()
        {
            var child = new Person("ребенок", Gender.Male);
            var father = new Person("папа", Gender.Male);

            child.SetFather(father);
            father.RemoveChild(child);

            Assert.True(child.Father == null);
        }

        [Fact]
        public void OneChildCanBeAddOnceToParent()
        {
            var child1 = new Person("ребенок", Gender.Male);
            var father = new Person("папа", Gender.Male);

            child1.SetFather(father);

            father.AddChild(child1);
            father.AddChild(child1);

            Assert.True(father.Childs.Count == 1);
        }

        private static string ConcatList(List<string> list)
        {
            return string.Join(";", list);
        }
    }
}