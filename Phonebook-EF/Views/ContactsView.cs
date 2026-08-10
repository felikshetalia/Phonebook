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

    public void DisplayContactsList(IReadOnlyCollection<Contact> contacts)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .AddColumn("Id")
            .AddColumn("First name")
            .AddColumn("Last name")
            .AddColumn("Email")
            .AddColumn("Phone Number");

        foreach (var contact in contacts)
            table.AddRow(
                contact.Id.ToString(),
                contact.FirstName,
                contact.LastName,
                contact.Email,
                contact.PhoneNumber);

        AnsiConsole.Write(table);
    }

    public ContactInfo AskForContactInfo()
    {
        var firstName = AnsiConsole.Ask<string>("Enter first name: ");
        var lastName = AnsiConsole.Ask<string>("Enter last name: ");
        var email = AnsiConsole.Ask<string>("Enter email: ");
        var phoneNo = AnsiConsole.Ask<string>("Enter phone number: ");

        return new ContactInfo
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNo
        };
    }
}
