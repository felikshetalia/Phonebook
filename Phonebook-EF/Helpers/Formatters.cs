public static class Formatters
{
    public static string FormatOption<T>(T option) where T : Enum
        => typeof(T) switch
        {
            Type t when t == typeof(ContactsMenuOption)
                => ContactsFormatOption((ContactsMenuOption)(object)option),
            Type t when t == typeof(MainMenuOption)
                => MainFormatOption((MainMenuOption)(object)option),
            Type t when t == typeof(CategoriesMenuOption)
                => CategoriesFormatOption((CategoriesMenuOption)(object)option),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, "Unsupported enum type")
        };

    private static string ContactsFormatOption(ContactsMenuOption option)
        => option switch
        {
            ContactsMenuOption.AddContact => "Add contact",
            ContactsMenuOption.ShowAllContacts => "Show all contacts",
            ContactsMenuOption.ShowContactDetails => "Show contact details",
            ContactsMenuOption.UpdateContactDetails => "Update contact details",
            ContactsMenuOption.DeleteContact => "Delete contact",
            ContactsMenuOption.Back => "Go back",
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };

    private static string CategoriesFormatOption(CategoriesMenuOption option)
        => option switch
        {
            CategoriesMenuOption.Family => "Family",
            CategoriesMenuOption.Friends => "Friends",
            CategoriesMenuOption.Work => "Work",
            CategoriesMenuOption.Other => "Other",
            CategoriesMenuOption.Back => "Go back",
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };

    private static string MainFormatOption(MainMenuOption option)
        => option switch
        {
            MainMenuOption.RunContacts => "Contacts",
            MainMenuOption.RunCategories => "Categories",
            MainMenuOption.SendEmail => "Send an email",
            MainMenuOption.SendSMS => "Send an SMS message",
            MainMenuOption.Exit => "Exit",
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
}