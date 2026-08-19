public sealed class EmailService
{
    private ContactsService _contactsService;

    public EmailService(ContactsService contactsService)
        => _contactsService = contactsService;
    public async Task<Email> CreateEmailMessage(string address, string? title, string body)
    {
        if (Validators.IsEmailBodyEmpty(body))
            throw new ArgumentException("Message body must exist");

        await _contactsService.GetContactByEmail(address);
        // if an exception will be thrown here it'll go to the controller
        return new Email(title, body);
    }
}