using System.Linq;
using Palindrome.Core.Validator;
using Palindrome.Core.ViewModel;
using NUnit.Framework;
using Shouldly;

namespace Palindrome.Test
{
    [TestFixture]
    public class PalindromeVmValidatorTest
    {
        PalindromeVmValidator validator;

        [SetUp]
        public void Init()
        {
            validator = new PalindromeVmValidator();
        }

        [TestCase("Was it a cat I saw?")]
        [TestCase("Don't nod.")]
        [TestCase("Radar")]
        [TestCase("No lemon, no melon")]
        public void GivenPalindromeVmValidator_WheValidViewModelPassed_ThenValidResultReturned(string phrase)
        {
            //Arrange
            var viewModel = new PalindromeVm
            {
                Phrase = phrase
            };

            //Act
            var result = validator.Validate(viewModel);

            //Assert
            result.IsValid.ShouldBeTrue();
        }
    }
}