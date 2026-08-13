using Spectre.Console;

namespace Phonebook_EF;

public sealed class CategoriesView : ICategoriesView
{
    public CategoriesMenuOption DisplayCategoriesMenu()
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(
            new SelectionPrompt<CategoriesMenuOption>()
                .Title("[purple]Categories[/]")
                .AddChoices(Enum.GetValues<CategoriesMenuOption>())
                .UseConverter(Formatters.FormatOption));
    }

    public void DisplayContactsList(IReadOnlyCollection<Contact> contacts)
    {
        throw new NotImplementedException();
    }

    public CategoriesMenuOption DisplayContactsMenu()
    {
        throw new NotImplementedException();
    }

    public void DisplayError(string message)
    {
        throw new NotImplementedException();
    }

    public void DisplayMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void WaitForInput()
    {
        throw new NotImplementedException();
    }
}