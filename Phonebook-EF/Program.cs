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
        ICategoriesView categoriesView = new CategoriesView();

        IContactRepository contactRepository = new ContactRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();

        ContactsService contactsService = new(contactRepository, categoryRepository);
        CategoriesService categoriesService = new(categoryRepository);

        CategoriesController categoriesController = new(categoriesView, categoriesService);
        ContactsController contactsController = new(contactsView, contactsService, categoriesController);
        AppController app = new(appView, contactsController, categoriesController);

        await app.Run();
    }
}