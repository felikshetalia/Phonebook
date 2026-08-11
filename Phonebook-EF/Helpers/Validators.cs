using System.Text.RegularExpressions;

public static class Validators
{
    public static bool IsContactNullOrDetailMissing(ContactInfo contact)
        => contact == null ||
            string.IsNullOrWhiteSpace(contact.FirstName) ||
            string.IsNullOrWhiteSpace(contact.LastName) ||
            string.IsNullOrWhiteSpace(contact.Email) ||
            string.IsNullOrWhiteSpace(contact.PhoneNumber);

    public static bool IsContactIdValid(string id, out int idAsInt)
        => int.TryParse(id, out idAsInt) && idAsInt > 0;

    public static bool IsEmailValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string patternCheck = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";

        return Regex.IsMatch(email, patternCheck, RegexOptions.IgnoreCase);
    }
}