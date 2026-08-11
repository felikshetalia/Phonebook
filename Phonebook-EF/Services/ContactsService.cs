public sealed class ContactsService
{
    private readonly IContactRepository _contactRepo;

    public ContactsService(IContactRepository repo)
    {
        _contactRepo = repo;
    }
    public async Task AddContact(ContactInfo contactInfo)
    {
        if (!Validators.IsEmailValid(contactInfo.Email))
            throw new Exception("The entered email is not valid");

        if (!TryMapContactInfoToDBModel(contactInfo, out Contact? contact))
            return;

        try
        {
            await _contactRepo.Add(contact!);
        }
        catch (OperationCanceledException e)
        {
            throw new Exception("Adding operation was unsuccessful" + e.Message);
        }
    }

    public async Task UpdateContact(string currentId, ContactInfo newDetails)
    {
        if (!Validators.IsContactIdValid(currentId, out int idAsInt))
            throw new Exception("The entered Id is invalid");

        if (!Validators.IsEmailValid(newDetails.Email))
            throw new Exception("The entered email is not valid");

        if (!TryMapContactInfoToDBModel(newDetails, out Contact? contact))
            return;

        try
        {
            await _contactRepo.Update(idAsInt, contact!);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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
    {
        try
        {
            return await _contactRepo.GetAll();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Contact> GetContactDetails(string contactId)
    {
        if (!Validators.IsContactIdValid(contactId, out int idAsInt))
            throw new Exception("The entered Id is invalid");

        try
        {
            return await _contactRepo.GetOne(idAsInt);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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