using NUnit.Framework;
using TinkerJems.Wpf.Application.Services;

namespace TinkerJems.WPF.Application.Tests
{
    public class Tests
    {
        private ValidationService _validationService;

        [SetUp]
        public void Setup()
        {
            _validationService = new ValidationService();
        }

        [TestCase("d@d.com", true)]
        [TestCase("asdfasdf", false)]
        [TestCase("123asd@sfg.com", true)]
        [TestCase("testemail@hotmail.com", true)]
        [TestCase(null, false)]
        public void UserEntersEmail_CorrectlyValidates(string customerEmail, bool valid)
        {
            bool IsValid = _validationService.ValidateEmail(customerEmail);
            Assert.AreEqual(valid, IsValid);
        }

        [TestCase(2, true)]
        [TestCase(-4, false)]
        [TestCase("Yes", false)]
        [TestCase(50, false)]
        [TestCase(0, false)]
        [TestCase("d12", false)]
        [TestCase(-1, false)]
        [TestCase(5, true)]
        [TestCase(5.2, false)]
        [TestCase(2.0, false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void UserEntersQuantity_QuantityIsValid(int quantity, bool valid)
        {
            bool IsValid = _validationService.ValidateQuantity(quantity);
            Assert.AreEqual(valid, IsValid);
        }
    }
}