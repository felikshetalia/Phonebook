public sealed class ContactsService
{
    private readonly IContactRepository _contactRepo;

    public ContactsService(IContactRepository repo)
    {
        _contactRepo = repo;
    }
    public async Task AddContact(ContactInfo contactInfo)
    {
        if (!TryMapContactInfoToDBModel(contactInfo, out Contact? contact))
            return;

        await _contactRepo.Add(contact!);
    }

    public async Task UpdateContact()
    {

    }

    public async Task DeleteContact(string contactId)
    {
        if (!Validators.IsContactIdValid(contactId, out int idAsInt))
            throw new Exception("The entered Id is invalid");

        try
        {
            await _contactRepo.Delete(idAsInt);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IReadOnlyCollection<Contact>> GetAllContacts()
        => await _contactRepo.GetAll();

    public async Task<Contact> GetContactDetails()
    {
        throw new NotImplementedException();
    }

    private static bool TryMapContactInfoToDBModel(ContactInfo info, out Contact? contact)
    {
        contact = null;

        if (!Validators.IsContactNullOrDetailMissing(info))
        {
            contact = new Contact
            {
                FirstName = info.FirstName,
                LastName = info.LastName,
                Email = info.Email,
                PhoneNumber = info.PhoneNumber
            };

            return true;
        }

        return false;
    }
}