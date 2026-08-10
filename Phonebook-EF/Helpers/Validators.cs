public static class Validators
{
    public static bool IsContactNullOrDetailMissing(ContactInfo contact)
        => contact == null ||
            string.IsNullOrWhiteSpace(contact.FirstName) ||
            string.IsNullOrWhiteSpace(contact.LastName) ||
            string.IsNullOrWhiteSpace(contact.Email) ||
            string.IsNullOrWhiteSpace(contact.PhoneNumber);
}