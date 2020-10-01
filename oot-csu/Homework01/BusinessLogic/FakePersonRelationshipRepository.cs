using Homework01.Domain;

namespace Homework01.BusinessLogic
{
    public class FakePersonRelationshipRepository
    {
        public PersonRelationshipRepository Create(Person me, Person mother, Person father)
        {
            var grandMotherM = new Person("бабушка мамы", Gender.Female);
            var grandFatherM = new Person("дедушка мамы", Gender.Male);

            var grandMotherF = new Person("бабушка папы", Gender.Female);
            var grandFatherF = new Person("дедушка папы", Gender.Male);

            var motherSister = new Person("сестра мамы", Gender.Female);
            var motherSisterHusband = new Person("муж сестры мамы", Gender.Male);
            var motherSisterChild = new Person("дочь сестры мамы", Gender.Female);
            var motherSisterChild2 = new Person("сын сестры мамы", Gender.Male);

            var fatherBrother = new Person("брат папы", Gender.Male);
            var fatherBrotherWife = new Person("жена брата папы", Gender.Female);
            var fatherBrotherChild = new Person("дочь брата папы", Gender.Female);

            var creator = new PersonRelationshipRepository();

            creator.AddMarriedCouple(mother, father);
            creator.AddMarriedCouple(motherSister, motherSisterHusband);
            creator.AddMarriedCouple(fatherBrother, fatherBrotherWife);
            creator.AddMarriedCouple(grandMotherM, grandFatherM);
            creator.AddMarriedCouple(grandMotherF, grandFatherF);

            creator.AddParent(me, mother, father);
            creator.AddParent(mother, grandMotherM, grandFatherM);
            creator.AddParent(father, grandMotherF, grandFatherF);

            creator.AddParent(motherSister, grandMotherM, grandFatherM);
            creator.AddParent(motherSisterChild, motherSister, motherSisterHusband);
            creator.AddParent(motherSisterChild2, motherSister, motherSisterHusband);

            creator.AddParent(fatherBrother, grandMotherF, grandFatherF);
            creator.AddParent(fatherBrotherChild, fatherBrother, fatherBrotherWife);

            return creator;
        }
    }
}