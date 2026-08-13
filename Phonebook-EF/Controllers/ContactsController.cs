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
                    await DisplayContactDetails();
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.UpdateContactDetails:
                    await UpdateContactDetails();
                    _contactsView.WaitForInput();
                    break;

                case ContactsMenuOption.DeleteContact:
                    await DeleteContact();
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
    }

    public async Task AddContact()
    {
        ContactInfo info = _contactsView.AskForContactInfo();
        try
        {
            await _service.AddContact(info);
            _contactsView.DisplayMessage("Contact added successfully.");
        }
        catch (Exception e)
        {
            _contactsView.DisplayError(e.Message);
        }
    }

    public async Task DisplayContacts()
    {
        IReadOnlyCollection<Contact> contactsList;

        try
        {
            contactsList = await _service.GetAllContacts();
            _contactsView.DisplayContactsList(contactsList);
        }
        catch (Exception e)
        {
            _contactsView.DisplayError(e.Message);
        }

    }

    public async Task DeleteContact()
    {
        await DisplayContacts();

        var id = _contactsView.AskForContactID("delete");

        try
        {
            await _service.DeleteContact(id);
            _contactsView.DisplayMessage("Contact deleted successfully.");
        }
        catch (Exception e)
        {
            _contactsView.DisplayError(e.Message);
        }
    }

    public async Task DisplayContactDetails()
    {
        await DisplayContacts();

        var id = _contactsView.AskForContactID("display");

        try
        {
            var contact = await _service.GetContactDetails(id);
            _contactsView.DisplayContactDetails(contact);
        }
        catch (Exception e)
        {
            _contactsView.DisplayError(e.Message);
        }
    }

    public async Task UpdateContactDetails()
    {
        await DisplayContacts();

        var id = _contactsView.AskForContactID("edit");
        try
        {
            var contact = await _service.GetContactDetails(id);
            ContactInfo info = _contactsView.AskForContactInfo();
            await _service.UpdateContact(id, info);
            _contactsView.DisplayMessage("Contact updated successfully.");
        }
        catch (Exception e)
        {
            _contactsView.DisplayError(e.Message);
        }
    }

}