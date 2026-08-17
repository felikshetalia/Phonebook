namespace Phonebook_EF;

using Spectre.Console;

public sealed class EmailView : IEmailView
{
    public string AskForDestinationEmail()
        => AnsiConsole.Ask<string>("To: ");

    public void DisplayError(string message)
        => AnsiConsole.MarkupLine($"[red]{Markup.Escape(message)}[/]");

    public void DisplayMessage(string message)
        => AnsiConsole.MarkupLine($"[green]{Markup.Escape(message)}[/]");

    public (string? title, string body) EnterEmailMessage()
    {
        string? title = AnsiConsole.Ask<string>("Enter message title (optional): ");
        string body = AnsiConsole.Ask<string>("Enter message body: ");

        return (title, body);
    }

    public void WaitForInput()
    {
        AnsiConsole.MarkupLine("\n[grey]Press any key to continue.[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}