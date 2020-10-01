using System.Collections.Generic;
using System.Linq;
using Homework01.BusinessLogic;
using Homework01.Domain;
using Xunit;

namespace Homework01
{
    public class UnitTests
    {
        static readonly Person _me = new Person("Я", Gender.Male);
        static readonly Person mother = new Person("мама", Gender.Female);
        static readonly Person father = new Person("папа", Gender.Male);
        static readonly PersonRelationshipRepository _repository = new FakePersonRelationshipRepository().Create(_me, mother, father);
        
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
            var partnerParents = _repository.GetPartnerParents(mother).Select(x => x.Name).ToList();
            var expected = new List<string>()
            {
                "бабушка папы",
                "дедушка папы",
            };
            Assert.Equal(ConcatList(expected), ConcatList(partnerParents));
        }

        private static string ConcatList(List<string> list)
        {
            return string.Join(";", list);
        }
    }
}