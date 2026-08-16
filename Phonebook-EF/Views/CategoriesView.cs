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
        AnsiConsole.Clear();

        var table = new Table()
            .AddColumn("Id")
            .AddColumn("First name")
            .AddColumn("Last name")
            .AddColumn("Email")
            .AddColumn("Phone Number")
            .AddColumn("Category");

        foreach (var contact in contacts)
            table.AddRow(
                contact.Id.ToString(),
                contact.FirstName,
                contact.LastName,
                contact.Email,
                contact.PhoneNumber,
                contact.Category!.Title);

        AnsiConsole.Write(table);
    }

    public void DisplayError(string message)
        => AnsiConsole.MarkupLine($"[red]{Markup.Escape(message)}[/]");

    public void DisplayMessage(string message)
        => AnsiConsole.MarkupLine($"[green]{Markup.Escape(message)}[/]");

    public void WaitForInput()
    {
        AnsiConsole.MarkupLine("\n[grey]Press any key to continue.[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}