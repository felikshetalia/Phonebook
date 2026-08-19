public sealed class ContactsService
{
    private readonly IContactRepository _contactRepo;
    private readonly ICategoryRepository _categoryRepo;

    public ContactsService(IContactRepository repo, ICategoryRepository categoryRepo)
    {
        _contactRepo = repo;
        _categoryRepo = categoryRepo;
    }
    public async Task AddContact(ContactInfo contactInfo)
    {
        if (!Validators.IsEmailValid(contactInfo.Email))
            throw new Exception("The entered email is not valid");

        if (!Validators.IsPhoneNumberValid(contactInfo.PhoneNumber))
            throw new Exception("The entered phone number is not valid");

        var contact = await MapContactInfoToDBModel(contactInfo);
        if (contact == null)
            throw new Exception("Couldn't perform update: Null field");

        try
        {
            await _contactRepo.Add(contact);
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

        if (!Validators.IsPhoneNumberValid(newDetails.PhoneNumber))
            throw new Exception("The entered phone number is not valid");

        var contact = await MapContactInfoToDBModel(newDetails);
        if (contact == null)
            throw new Exception("Couldn't perform update: Null field");

        try
        {
            await _contactRepo.Update(idAsInt, contact);
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

    public async Task<Contact> GetContactByEmail(string email)
    {
        if (!Validators.IsEmailValid(email))
            throw new Exception("The entered email is not valid");

        var contactList = await GetAllContacts();

        var contact = contactList.FirstOrDefault(
            c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (contact == null)
            throw new Exception("No contact with that email was found.");

        return contact;
    }

    public async Task<Contact> GetContactByPhoneNumber(string phone)
    {
        if (!Validators.IsPhoneNumberValid(phone))
            throw new Exception("The phone number is not valid");

        var contactList = await GetAllContacts();

        var contact = contactList.FirstOrDefault(
            c => c.PhoneNumber.Equals(phone));

        if (contact == null)
            throw new Exception("No contact with that phone number was found.");

        return contact;
    }

    private async Task<Contact?> MapContactInfoToDBModel(ContactInfo info)
    {
        if (Validators.IsContactNullOrDetailMissing(info))
            return null;

        var categoryId = await GetCategoryIdByTitle(info.Category);

        return new Contact
        {
            FirstName = info.FirstName,
            LastName = info.LastName,
            Email = info.Email,
            PhoneNumber = info.PhoneNumber,
            CategoryId = categoryId
        };
    }

    private async Task<int> GetCategoryIdByTitle(string categoryTitle)
    {
        var categories = await _categoryRepo.GetAll();
        var category = categories.FirstOrDefault(c => c.Title == categoryTitle);

        if (category == null)
            throw new Exception($"Category '{categoryTitle}' not found");

        return category.Id;
    }
}