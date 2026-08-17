using System.Threading.Tasks;

namespace Phonebook_EF;

public sealed class EmailController
{
    private IEmailView _emailView;
    private EmailService _emailService;

    public EmailController(IEmailView emailView, EmailService service)
    {
        _emailView = emailView;
        _emailService = service;
    }

    public async Task Run()
    {
        var dest = _emailView.AskForDestinationEmail();
        var msg = _emailView.EnterEmailMessage();

        try
        {
            await _emailService.CreateEmailMessage(dest, msg.title, msg.body);
            _emailView.DisplayMessage("Email sent successfully");
        }
        catch (ArgumentException e)
        {
            _emailView.DisplayError(e.Message);
        }
        catch (Exception e)
        {
            _emailView.DisplayError(e.Message);
        }
        _emailView.WaitForInput();

    }
}