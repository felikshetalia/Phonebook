using Spectre.Console;

namespace Phonebook_EF;

public sealed class AppView : IAppView
{
    public MainMenuOption DisplayMainMenu()
    {
        AnsiConsole.Clear();

        return AnsiConsole.Prompt(
        new SelectionPrompt<MainMenuOption>()
        .Title("[purple]My phonebook[/]")
        .AddChoices(Enum.GetValues<MainMenuOption>())
        .UseConverter(Formatters.FormatOption)
        );
    }
    public void DisplayGoodbye()
        => AnsiConsole.MarkupLine("[yellow]Goodbye![/]");
    public void DisplayMessage(string message)
        => AnsiConsole.MarkupLine($"[green]{Markup.Escape(message)}[/]");

    public void WaitForInput()
    {
        AnsiConsole.MarkupLine("\n[grey]Press any key to continue.[/]");
        AnsiConsole.Console.Input.ReadKey(true);
    }
}