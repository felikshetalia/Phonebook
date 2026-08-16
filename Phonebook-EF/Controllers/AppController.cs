using System.Threading.Tasks;

namespace Phonebook_EF;

public sealed class AppController
{
    private readonly IAppView _appView;
    private readonly ContactsController _contactsController;
    private readonly CategoriesController _categoryController;

    public AppController(IAppView view, ContactsController contactsController, CategoriesController categoryController)
    {
        _appView = view;
        _contactsController = contactsController;
        _categoryController = categoryController;
    }

    public async Task Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            MainMenuOption selectedOption = _appView.DisplayMainMenu();

            switch (selectedOption)
            {
                case MainMenuOption.RunContacts:
                    await _contactsController.Run();
                    break;
                case MainMenuOption.RunCategories:
                    await _categoryController.Run();
                    break;
                case MainMenuOption.Exit:
                    isRunning = false;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(selectedOption),
                        selectedOption,
                        "Unknown menu option.");
            }
        }
        _appView.DisplayGoodbye();
    }
}