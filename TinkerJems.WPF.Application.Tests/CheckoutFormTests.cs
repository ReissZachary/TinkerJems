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
        [TestCase(51, false)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        [TestCase(5, true)]
        public void UserEntersQuantity_QuantityIsValid(int quantity, bool valid)
        {
            bool IsValid = _validationService.ValidateQuantity(quantity);
            Assert.AreEqual(valid, IsValid);
        }

        [TestCase("small", "Ring", false)]
        [TestCase("6", "Ring", true)]
        [TestCase("5.5", "Ring", true)]
        [TestCase("asdfa", "Ring", false)]
        [TestCase("14", "Ring", true)]
        [TestCase("14.5", "Ring", false)]
        [TestCase("0.5", "Ring", true)]
        [TestCase("55", "Ring", false)]
        [TestCase("0", "Ring", false)]
        [TestCase("36 inches", "Necklace", true)]
        [TestCase("small", "Necklace", false)]
        [TestCase("14 inches", "Necklace", true)]
        [TestCase("asdf", "Necklace", false)]
        [TestCase("Large", "Necklace", false)]
        [TestCase("large", "Bracelet", false)]
        [TestCase("Extra Large: 23-25 cm", "Bracelet", true)]
        [TestCase("asdf", "Bracelet", false)]
        [TestCase("7.7", "Bracelet", false)]
        [TestCase("12", "Bracelet", false)]
        [TestCase("Small", "Earring", true)]
        [TestCase("6", "Earring", false)]
        [TestCase("5.5", "Earring", false)]
        [TestCase("Large", "Earring", true)]
        [TestCase("0", "Earring", false)]
        public void UserEntersSize_SizeIsValidated(string size, string category, bool valid)
        {
            var List = _validationService.GetAllSizesByCategory(category);
            Assert.That(List.Contains(size) == valid);
        }
    }
}