using Spectre.Console;

namespace Phonebook_EF;

public sealed class ContactsView : IContactsView
{
    public ContactsMenuOption DisplayContactsMenu()
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(
            new SelectionPrompt<ContactsMenuOption>()
                .Title("[purple]Contacts[/]")
                .AddChoices(Enum.GetValues<ContactsMenuOption>())
                .UseConverter(Formatters.FormatOption)
        );
    }

    public void DisplayMessage(string message)
        => AnsiConsole.MarkupLine($"[green]{Markup.Escape(message)}[/]");

    public void DisplayGoodbye()
        => AnsiConsole.MarkupLine("[yellow]Goodbye![/]");

    public void WaitForInput()
    {
        AnsiConsole.MarkupLine("\n[grey]Press any key to continue.[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}
