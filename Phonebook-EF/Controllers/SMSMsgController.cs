using Phonebook_EF;

public sealed class SMSMsgController
{
    private ISMSView _smsView;
    private SMSMsgService _smsService;

    public SMSMsgController(ISMSView smsView, SMSMsgService service)
    {
        _smsView = smsView;
        _smsService = service;
    }

    public async Task Run()
    {
        var dest = _smsView.AskForDestinationPhone();
        var msg = _smsView.EnterTextMessage();

        try
        {
            await _smsService.CreateSMSMessage(dest, msg);
            _smsView.DisplayMessage("Message sent successfully");
        }
        catch (ArgumentException e)
        {
            _smsView.DisplayError(e.Message);
        }
        catch (Exception e)
        {
            _smsView.DisplayError(e.Message);
        }
        _smsView.WaitForInput();

    }
}