namespace Phonebook_EF;

public interface IContactsView
{
    ContactsMenuOption DisplayContactsMenu();
    void DisplayMessage(string message);
    void DisplayGoodbye();
    void WaitForInput();
    void DisplayContactsList(IReadOnlyCollection<Contact> contacts);
    ContactInfo AskForContactInfo();
}
