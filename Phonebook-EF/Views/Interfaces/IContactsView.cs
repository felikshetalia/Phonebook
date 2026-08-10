namespace Phonebook_EF;

public interface IContactsView
{
    ContactsMenuOption DisplayContactsMenu();
    void DisplayMessage(string message);
    void DisplayGoodbye();
    void DisplayError(string message);
    void WaitForInput();
    void DisplayContactsList(IReadOnlyCollection<Contact> contacts);
    void DisplayContactDetails(Contact contact);
    ContactInfo AskForContactInfo();
    string AskForContactID(string mode);
}
