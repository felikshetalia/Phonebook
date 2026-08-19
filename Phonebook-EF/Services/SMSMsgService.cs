public sealed class SMSMsgService
{
    private ContactsService _contactsService;

    public SMSMsgService(ContactsService contactsService)
        => _contactsService = contactsService;
    public async Task<SMS> CreateSMSMessage(string phoneNo, string text)
    {
        if (Validators.IsSMSBodyEmpty(text))
            throw new ArgumentException("You cannot send an empty message.");

        await _contactsService.GetContactByPhoneNumber(phoneNo);
        // if an exception will be thrown here it'll go to the controller
        return new SMS(text);
    }
}