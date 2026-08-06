namespace Phonebook_EF;

class Program
{
    static void Main(string[] args)
    {
        PhonebookContext.InitializeDatabase();

        IAppView appView = new AppView();
        IContactsView contactsView = new ContactsView();

        ContactsController contactsController = new(contactsView);
        AppController app = new(appView, contactsController);

        app.Run();
    }
}