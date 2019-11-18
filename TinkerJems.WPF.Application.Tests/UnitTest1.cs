using NUnit.Framework;
using TinkerJems.Wpf.Application.Services;

namespace TinkerJems.WPF.Application.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("d@d.com", true)]
        [TestCase("asdfasdf", false)]
        [TestCase("123asd@sfg.com", true)]
        [TestCase("testemail@hotmail.com", true)]
        public void UserEntersEmail_CorrectlyValidates(string customerEmail, bool valid)
        {
            var service = new ValidationService();
            bool IsValid = service.ValidateEmail(customerEmail);
            Assert.AreEqual(valid, IsValid);
        }
    }
}