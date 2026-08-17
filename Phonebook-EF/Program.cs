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
        IEmailView emailView = new EmailView();

        IContactRepository contactRepository = new ContactRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();

        ContactsService contactsService = new(contactRepository, categoryRepository);
        CategoriesService categoriesService = new(categoryRepository);
        EmailService emailService = new(contactsService);

        CategoriesController categoriesController = new(categoriesView, categoriesService);
        ContactsController contactsController = new(contactsView, contactsService, categoriesController);
        EmailController emailController = new(emailView, emailService);
        AppController app = new(appView, contactsController, categoriesController, emailController);

        await app.Run();
    }
}