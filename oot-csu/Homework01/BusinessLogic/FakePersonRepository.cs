using Homework01.Domain;

namespace Homework01.BusinessLogic
{
    public class FakePersonRepository
    {
        public PersonRepository Create(Person me, Person mother, Person father)
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

            me.SetMother(mother);
            me.SetFather(father);

            mother.SetMother(grandMotherM);
            mother.SetFather(grandFatherM);

            father.SetMother(grandMotherF);
            father.SetFather(grandFatherF);

            motherSister.SetMother(grandMotherM);
            motherSister.SetFather(grandFatherM);

            motherSisterChild.SetMother(motherSister);
            motherSisterChild.SetFather(motherSisterHusband);

            motherSisterChild2.SetMother(motherSister);
            motherSisterChild2.SetFather(motherSisterHusband);

            fatherBrother.SetMother(grandMotherF);
            fatherBrother.SetFather(grandFatherF);

            fatherBrotherChild.SetMother(fatherBrotherWife);
            fatherBrotherChild.SetFather(fatherBrother);

            mother.SetPartner(father);
            father.SetPartner(mother);

            motherSister.SetPartner(motherSisterHusband);
            motherSisterHusband.SetPartner(motherSister);

            fatherBrotherWife.SetPartner(fatherBrother);
            fatherBrother.SetPartner(fatherBrotherWife);

            grandMotherM.SetPartner(grandFatherM);
            grandFatherM.SetPartner(grandMotherM);

            grandMotherF.SetPartner(grandFatherF);
            grandFatherF.SetPartner(grandMotherF);

            return new PersonRepository();
        }
    }
}