using Fix;
using IdentityServer.UnitTest.Facades;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer.UnitTest
{
    public class HumanizerTests : TestBase
    {
        public HumanizerTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Username_create_test()
        {
            //var nameString = "Ahmet Çağrı AKCA";
            //var toEnglish = String.Join("", "Ahmet Çağrıqwertyuıopğüasdfghjklşi,zxcvbnmöç.QWERTYUIOPĞÜASDFGHJKLŞİZXCVBNMÖÇ. AKCA".Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ı", "i");
            //var testPlurize=nameString.Pluralize();
            // testPlurize=nameString.Normalize();
            // testPlurize=nameString.ToLowerInvariant();
            // testPlurize=nameString.Hyphenate();
            // testPlurize=nameString.Kebaberize();
            // testPlurize=nameString.Pluralize();
            // testPlurize=nameString.Underscore();
            // testPlurize=nameString.ApplyCase(LetterCasing.AllCaps);
            // testPlurize=nameString.ApplyCase(LetterCasing.Sentence);
            // testPlurize=nameString.ApplyCase(LetterCasing.LowerCase);
            // testPlurize=nameString.ApplyCase(LetterCasing.Title);
            // testPlurize=nameString.Camelize();
            // testPlurize =nameString.Humanize();
            // testPlurize=nameString.ToQuantity(2,ShowQuantityAs.Words);
            // testPlurize=nameString.Transform();
            // testPlurize=nameString.ToUpperInvariant();
            // testPlurize=nameString.Dasherize();
            // testPlurize=nameString.Dehumanize();
            // testPlurize=nameString.Ordinalize();
            // testPlurize=nameString.Pascalize();
            // testPlurize=nameString.Singularize();
            Assert.True("Ahmet Çağrı AKCA".ToEnglishCharacter() == "Ahmet Cagri AKCA");
            Assert.True("Ahmet Çağrı AKCA".GetFirstWord() == "Ahmet");
            Assert.True("Mqwertyuıopğüasdfghjklşizxcvbnmöç".GetFirstWord().ToEnglishCharacter() == "Mqwertyuiopguasdfghjklsizxcvbnmoc");
            //Acc

        }

    }
}
