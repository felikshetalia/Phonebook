namespace Phonebook_EF;

public interface IEmailView
{
    string AskForDestinationEmail();
    (string? title, string body) EnterEmailMessage();
    void DisplayMessage(string message);
    void DisplayError(string message);
    void WaitForInput();

}