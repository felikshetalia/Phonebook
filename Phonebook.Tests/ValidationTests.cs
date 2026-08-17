namespace Phonebook.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        private static readonly IEnumerable<TestCaseData> ContactDetailsTestCases = new[]
        {
            new TestCaseData((ContactInfo?)null, true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenContactIsNull"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "John",
                    LastName = "",
                    Email = "john@example.com",
                    PhoneNumber = "+441234567890",
                    Category = "Family"
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenLastNameIsEmpty"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "+441234567890",
                    Category = ""
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenCategoryIsEmpty"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "+441234567890",
                    Category = "   "
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenCategoryIsWhitespace"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "+441234567890",
                    Category = "Friends"
                },
                false)
                .SetName("IsContactNullOrDetailMissing_ReturnsFalse_WhenAllDetailsArePresent"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "+441234567890",
                    Category = "Friends"
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenFirstNameIsEmpty"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "   ",
                    PhoneNumber = "+441234567890",
                    Category = "Friends"
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenEmailIsWhitespace"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "",
                    Category = "Friends"
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenPhoneNumberIsEmpty"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "",
                    LastName = "",
                    Email = "",
                    PhoneNumber = "",
                    Category = ""
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenAllFieldsAreEmpty"),

            new TestCaseData(
                new ContactInfo
                {
                    FirstName = "   ",
                    LastName = "   ",
                    Email = "   ",
                    PhoneNumber = "   ",
                    Category = "   "
                },
                true)
                .SetName("IsContactNullOrDetailMissing_ReturnsTrue_WhenAllFieldsAreWhitespace")
        };

        private static readonly IEnumerable<TestCaseData> ValidContactIdTestCases = new[]
        {
            new TestCaseData("42", 42)
                .SetName("IsContactIdValid_ReturnsTrue_ForPositiveId"),

            new TestCaseData("999999", 999999)
                .SetName("IsContactIdValid_ReturnsTrue_ForLargePositiveId")
        };

        private static readonly IEnumerable<TestCaseData> InvalidContactIdTestCases = new[]
        {
            new TestCaseData("0")
                .SetName("IsContactIdValid_ReturnsFalse_ForZero"),

            new TestCaseData("-5")
                .SetName("IsContactIdValid_ReturnsFalse_ForNegative"),

            new TestCaseData("-100")
                .SetName("IsContactIdValid_ReturnsFalse_ForLargeNegative"),

            new TestCaseData("abc")
                .SetName("IsContactIdValid_ReturnsFalse_ForNonNumeric"),

            new TestCaseData("12.5")
                .SetName("IsContactIdValid_ReturnsFalse_ForDecimal"),

            new TestCaseData("")
                .SetName("IsContactIdValid_ReturnsFalse_ForEmpty"),

            new TestCaseData("   ")
                .SetName("IsContactIdValid_ReturnsFalse_ForWhitespace"),

            new TestCaseData("2147483648")
                .SetName("IsContactIdValid_ReturnsFalse_WhenAboveIntMaxValue"),

            new TestCaseData("-2147483649")
                .SetName("IsContactIdValid_ReturnsFalse_WhenBelowIntMinValue"),
        };

        private static readonly IEnumerable<TestCaseData> EmailValidationTestCases = new[]
        {
            new TestCaseData("user@example.com", true)
                .SetName("IsEmailValid_ReturnsTrue_ValidSimple"),

            new TestCaseData("user.name+tag@example.co.uk", true)
                .SetName("IsEmailValid_ReturnsTrue_WithDotAndPlus"),

            new TestCaseData("test.email@sub.domain.com", true)
                .SetName("IsEmailValid_ReturnsTrue_WithSubdomain"),

            new TestCaseData("a@b.co", true)
                .SetName("IsEmailValid_ReturnsTrue_Short"),

            new TestCaseData("user123@test-domain.com", true)
                .SetName("IsEmailValid_ReturnsTrue_WithHyphen"),

            new TestCaseData("user..name@example.com", true)
                .SetName("IsEmailValid_ReturnsTrue_DoubleDot"),

            new TestCaseData("", false)
                .SetName("IsEmailValid_ReturnsFalse_Empty"),

            new TestCaseData("   ", false)
                .SetName("IsEmailValid_ReturnsFalse_Whitespace"),

            new TestCaseData("invalid-email", false)
                .SetName("IsEmailValid_ReturnsFalse_NoAt"),

            new TestCaseData("user@", false)
                .SetName("IsEmailValid_ReturnsFalse_NoDomain"),

            new TestCaseData("@example.com", false)
                .SetName("IsEmailValid_ReturnsFalse_NoLocalPart"),

            new TestCaseData("user@localhost", false)
                .SetName("IsEmailValid_ReturnsFalse_NoTld"),

            new TestCaseData("user @example.com", false)
                .SetName("IsEmailValid_ReturnsFalse_WithSpace"),

            new TestCaseData("user@.com", false)
                .SetName("IsEmailValid_ReturnsFalse_NoDomainName")
        };

        private static readonly IEnumerable<TestCaseData> EmailBodyValidationTestCases = new[]
        {
            new TestCaseData("Hello!", false)
                .SetName("IsEmailBodyValid_ReturnsTrue_ForSimpleBody"),

            new TestCaseData("This is an email message.", false)
                .SetName("IsEmailBodyValid_ReturnsTrue_ForSentence"),

            new TestCaseData("Hello,\nHow are you?\nRegards", false)
                .SetName("IsEmailBodyValid_ReturnsTrue_ForMultilineBody"),

            new TestCaseData("a", false)
                .SetName("IsEmailBodyValid_ReturnsTrue_ForSingleCharacter"),

            new TestCaseData("", true)
                .SetName("IsEmailBodyValid_ReturnsFalse_ForEmptyBody"),

            new TestCaseData("   ", true)
                .SetName("IsEmailBodyValid_ReturnsFalse_ForWhitespaceBody"),

            new TestCaseData("\t", true)
                .SetName("IsEmailBodyValid_ReturnsFalse_ForTabOnlyBody"),

            new TestCaseData("\n\r", true)
                .SetName("IsEmailBodyValid_ReturnsFalse_ForNewlineOnlyBody"),

            new TestCaseData(null, true)
                .SetName("IsEmailBodyValid_ReturnsFalse_ForNullBody")
        };

        private static readonly IEnumerable<TestCaseData> PhoneNumberValidationTestCases = new[]
        {
            new TestCaseData("+12345678901", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_InternationalPlus11"),

            new TestCaseData("12345678901", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_NoPlus11"),

            new TestCaseData("+1234567890", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_InternationalPlus10"),

            new TestCaseData("5551234567", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_NoPlus10"),

            new TestCaseData("+441234567890", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_UK"),

            new TestCaseData("+33123456789", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_France"),

            new TestCaseData("9876543210", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_ValidFormat"),

            new TestCaseData("", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_Empty"),

            new TestCaseData("   ", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_Whitespace"),

            new TestCaseData("+0123456", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_LeadingZero"),

            new TestCaseData("phone123", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_Letters"),

            new TestCaseData("+12345", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_TooShort"),

            new TestCaseData("123", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_VeryShort"),

            new TestCaseData("+", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_OnlyPlus"),

            new TestCaseData("123-456-7890", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_WithDashes"),

            new TestCaseData("(123) 456-7890", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_WithParens"),

            new TestCaseData("abc123def456", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_MixedLettersNumbers"),

            new TestCaseData("12345678", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_AtMinimumLength"),

            new TestCaseData("123456789012345", true)
                .SetName("IsPhoneNumberValid_ReturnsTrue_AtMaximumLength"),

            new TestCaseData("1234567", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_BelowMinimumLength"),

            new TestCaseData("1234567890123456", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_AboveMaximumLength"),

            new TestCaseData("0123456789", false)
                .SetName("IsPhoneNumberValid_ReturnsFalse_WhenStartingWithZero"),
        };


        [TestCaseSource(nameof(ContactDetailsTestCases))]
        public void IsContactNullOrDetailMissing_ReturnsCorrectResult(ContactInfo? contact, bool expected)
        {
            bool result = Validators.IsContactNullOrDetailMissing(contact!);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(ValidContactIdTestCases))]
        public void IsContactIdValid_ReturnsTrueAndParsesId(string input, int expectedId)
        {
            bool result = Validators.IsContactIdValid(input, out int id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(id, Is.EqualTo(expectedId));
            });
        }

        [TestCaseSource(nameof(InvalidContactIdTestCases))]
        public void IsContactIdValid_ReturnsFalseForInvalidId(string input)
        {
            bool result = Validators.IsContactIdValid(input, out _);
            Assert.That(result, Is.False);
        }

        [TestCaseSource(nameof(EmailValidationTestCases))]
        public void IsEmailValid_ReturnsCorrectResult(string email, bool expected)
        {
            bool result = Validators.IsEmailValid(email);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(PhoneNumberValidationTestCases))]
        public void IsPhoneNumberValid_ReturnsCorrectResult(string phoneNumber, bool expected)
        {
            bool result = Validators.IsPhoneNumberValid(phoneNumber);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(EmailBodyValidationTestCases))]
        public void IsEmailBodyValid_ReturnsCorrectResult(string body, bool expected)
        {
            bool result = Validators.IsEmailBodyEmpty(body);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}