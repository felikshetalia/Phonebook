using System.Threading.Tasks;

namespace Phonebook_EF;

public sealed class ContactsController
{
    private readonly IContactsView _contactsView;
    private readonly ContactsService _service;

    public ContactsController(IContactsView contactsView, ContactsService service)
    {
        _contactsView = contactsView;
        _service = service;
    }
    public async Task Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            ContactsMenuOption selectedOption = _contactsView.DisplayContactsMenu();

            switch (selectedOption)
            {
                case ContactsMenuOption.AddContact:
                    await AddContact();
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.ShowAllContacts:
                    await DisplayContacts();
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

    public async Task AddContact()
    {
        ContactInfo info = _contactsView.AskForContactInfo();
        await _service.AddContact(info);
    }

    public async Task DisplayContacts()
    {
        IReadOnlyCollection<Contact> contactsList =
            await _service.GetAllContacts();

        _contactsView.DisplayContactsList(contactsList);
    }

}