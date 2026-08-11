using System.Threading.Tasks;

namespace Phonebook_EF;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            PhonebookContext.InitializeDatabase();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Database initialization failed: {e.Message}");
            return;
        }

        IAppView appView = new AppView();
        IContactsView contactsView = new ContactsView();

        IContactRepository contactRepository = new ContactRepository();

        ContactsService contactsService = new(contactRepository);

        ContactsController contactsController = new(contactsView, contactsService);
        AppController app = new(appView, contactsController);

        await app.Run();
    }
}