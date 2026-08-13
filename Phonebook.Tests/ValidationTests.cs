namespace Phonebook.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void IsContactNullOrDetailMissing_ReturnsTrue_WhenContactIsNull()
        {
            bool result = Validators.IsContactNullOrDetailMissing(null!);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsContactNullOrDetailMissing_ReturnsTrue_WhenAnyDetailIsMissing()
        {
            var contact = new ContactInfo
            {
                FirstName = "John",
                LastName = "",
                Email = "john@example.com",
                PhoneNumber = "+441234567890"
            };

            bool result = Validators.IsContactNullOrDetailMissing(contact);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsContactNullOrDetailMissing_ReturnsFalse_WhenAllDetailsArePresent()
        {
            var contact = new ContactInfo
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = "+441234567890"
            };

            bool result = Validators.IsContactNullOrDetailMissing(contact);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsContactIdValid_ReturnsTrueAndParsesPositiveId()
        {
            bool result = Validators.IsContactIdValid("42", out int idAsInt);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(idAsInt, Is.EqualTo(42));
            });
        }

        [TestCase("0")]
        [TestCase("-5")]
        [TestCase("abc")]
        public void IsContactIdValid_ReturnsFalseForInvalidOrNegativeId(string input)
        {
            bool result = Validators.IsContactIdValid(input, out int id);

            Assert.That(result, Is.False);
        }

        [TestCase("user@example.com", true)]
        [TestCase("user.name+tag@example.co.uk", true)]
        [TestCase("", false)]
        [TestCase("   ", false)]
        [TestCase("invalid-email", false)]
        [TestCase("user@localhost", false)]
        public void IsEmailValid_ReturnsCorrectResult(string email, bool expected)
        {
            Assert.That(Validators.IsEmailValid(email), Is.EqualTo(expected));
        }

        [TestCase("+12345678901", true)]
        [TestCase("1234567890", true)]
        [TestCase("", false)]
        [TestCase("   ", false)]
        [TestCase("+0123456", false)]
        [TestCase("phone123", false)]
        [TestCase("+12345", false)]
        public void IsPhoneNumberValid_ReturnsCorrectResult(string phoneNumber, bool expected)
        {
            Assert.That(Validators.IsPhoneNumberValid(phoneNumber), Is.EqualTo(expected));
        }
    }
}
