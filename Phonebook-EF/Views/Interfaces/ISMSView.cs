namespace Phonebook_EF;

public interface ISMSView
{
    string AskForDestinationPhone();
    string EnterTextMessage();
    void DisplayMessage(string message);
    void DisplayError(string message);
    void WaitForInput();

}