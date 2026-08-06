namespace Phonebook_EF;

public sealed class AppController
{
    private readonly IAppView _appView;
    private readonly ContactsController _contactsController;

    public AppController(IAppView view, ContactsController contactsController)
    {
        _appView = view;
        _contactsController = contactsController;
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            MainMenuOption selectedOption = _appView.DisplayMainMenu();

            switch (selectedOption)
            {
                case MainMenuOption.RunContacts:
                    _contactsController.Run();
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