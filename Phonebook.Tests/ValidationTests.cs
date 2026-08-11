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

        [Test]
        public void IsContactIdValid_ReturnsFalseForInvalidOrNegativeId()
        {
            bool result1 = Validators.IsContactIdValid("0", out int id1);
            bool result2 = Validators.IsContactIdValid("-5", out int id2);
            bool result3 = Validators.IsContactIdValid("abc", out int id3);

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.False);
                Assert.That(result2, Is.False);
                Assert.That(result3, Is.False);
            });
        }

        [Test]
        public void IsEmailValid_ReturnsTrueForValidEmailStrings()
        {
            Assert.That(Validators.IsEmailValid("user@example.com"), Is.True);
            Assert.That(Validators.IsEmailValid("user.name+tag@example.co.uk"), Is.True);
        }

        [Test]
        public void IsEmailValid_ReturnsFalseForInvalidOrEmptyEmails()
        {
            Assert.That(Validators.IsEmailValid(""), Is.False);
            Assert.That(Validators.IsEmailValid("   "), Is.False);
            Assert.That(Validators.IsEmailValid("invalid-email"), Is.False);
            Assert.That(Validators.IsEmailValid("user@localhost"), Is.False);
        }

        [Test]
        public void IsPhoneNumberValid_ReturnsTrueForValidPhoneNumbers()
        {
            Assert.That(Validators.IsPhoneNumberValid("+12345678901"), Is.True);
            Assert.That(Validators.IsPhoneNumberValid("1234567890"), Is.True);
        }

        [Test]
        public void IsPhoneNumberValid_ReturnsFalseForInvalidOrEmptyPhones()
        {
            Assert.That(Validators.IsPhoneNumberValid(""), Is.False);
            Assert.That(Validators.IsPhoneNumberValid("   "), Is.False);
            Assert.That(Validators.IsPhoneNumberValid("+0123456"), Is.False);
            Assert.That(Validators.IsPhoneNumberValid("phone123"), Is.False);
            Assert.That(Validators.IsPhoneNumberValid("+12345"), Is.False);
        }
    }
}
