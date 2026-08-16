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
            .AddColumn("Phone Number")
            .AddColumn("Category");

        foreach (var contact in contacts)
            table.AddRow(
                contact.Id.ToString(),
                contact.FirstName,
                contact.LastName,
                contact.Email,
                contact.PhoneNumber,
                contact.Category == null ? "" : contact.Category.Title);

        AnsiConsole.Write(table);
    }

    public ContactInfo AskForContactInfo(IReadOnlyCollection<Category> categoryList)
    {
        var firstName = AnsiConsole.Ask<string>("Enter first name: ");
        var lastName = AnsiConsole.Ask<string>("Enter last name: ");
        var email = AnsiConsole.Ask<string>("Enter email (e.g. name@example.com): ");
        var phoneNo = AnsiConsole.Ask<string>("Enter phone number (8-15 digits, optional leading +, no spaces): ");
        var category = AnsiConsole.Prompt(new SelectionPrompt<Category>()
            .Title("Category: ")
            .AddChoices(categoryList)
            .UseConverter(c => c.Title));

        return new ContactInfo
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNo,
            Category = category.Title
        };
    }

    public string AskForContactID(string mode)
        => AnsiConsole.Ask<string>($"Enter the ID of the contact you want to {mode}: ");

    public void DisplayError(string message)
        => AnsiConsole.MarkupLine($"[red]{Markup.Escape(message)}[/]");

    public void DisplayContactDetails(Contact contact)
    {
        AnsiConsole.Clear();

        var table = new Table()
            .AddColumn("Id")
            .AddColumn("First name")
            .AddColumn("Last name")
            .AddColumn("Email")
            .AddColumn("Phone Number")
            .AddColumn("Category");

        table.AddRow(
            contact.Id.ToString(),
            contact.FirstName,
            contact.LastName,
            contact.Email,
            contact.PhoneNumber,
            contact.Category!.Title);

        AnsiConsole.Write(table);
    }
}
