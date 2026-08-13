namespace Phonebook_EF;

public interface ICategoriesView
{
    CategoriesMenuOption DisplayCategoriesMenu();
    void DisplayMessage(string message);
    void DisplayError(string message);
    void WaitForInput();
    void DisplayContactsList(IReadOnlyCollection<Contact> contacts);
}