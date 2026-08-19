namespace Phonebook_EF;

using Spectre.Console;

public sealed class SMSView : ISMSView
{
    public string AskForDestinationPhone()
        => AnsiConsole.Ask<string>("To: ");

    public void DisplayError(string message)
        => AnsiConsole.MarkupLine($"[red]{Markup.Escape(message)}[/]");

    public void DisplayMessage(string message)
        => AnsiConsole.MarkupLine($"[green]{Markup.Escape(message)}[/]");

    public string EnterTextMessage()
        => AnsiConsole.Ask<string>("Enter text: ");

    public void WaitForInput()
    {
        AnsiConsole.MarkupLine("\n[grey]Press any key to continue.[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}