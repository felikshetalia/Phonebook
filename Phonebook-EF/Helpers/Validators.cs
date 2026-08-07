public static class Validators
{
    public static bool IsContactNullOrDetailMissing(Contact contact)
        => contact == null ||
            string.IsNullOrWhiteSpace(contact.FirstName) ||
            string.IsNullOrWhiteSpace(contact.LastName) ||
            string.IsNullOrWhiteSpace(contact.Email) ||
            string.IsNullOrWhiteSpace(contact.PhoneNumber) ||
            contact.Id < 0;
}