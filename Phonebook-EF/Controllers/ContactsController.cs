namespace Phonebook_EF;

public sealed class ContactsController
{
    private readonly IContactsView _contactsView;

    public ContactsController(IContactsView contactsView)
    {
        _contactsView = contactsView;
    }
    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            ContactsMenuOption selectedOption = _contactsView.DisplayContactsMenu();

            switch (selectedOption)
            {
                case ContactsMenuOption.AddContact:
                    _contactsView.DisplayMessage("This field is for adding contacts");
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.ShowAllContacts:
                    _contactsView.DisplayMessage("This field is for showing all contacts");
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.ShowContactDetails:
                    _contactsView.DisplayMessage("This field is for showing contact details");
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.UpdateContactDetails:
                    _contactsView.DisplayMessage("This field is for updating contact details");
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.DeleteContact:
                    _contactsView.DisplayMessage("This field is for deleting contacts");
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.Back:
                    isRunning = false;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(selectedOption),
                        selectedOption,
                        "Unknown menu option.");
            }
        }
        _contactsView.DisplayGoodbye();
    }
}