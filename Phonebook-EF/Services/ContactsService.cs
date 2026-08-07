using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public sealed class ContactsService
{
    private readonly IContactRepository _contactRepo;

    public ContactsService(IContactRepository repo)
    {
        _contactRepo = repo;
    }
    public async Task AddContact()
    {

    }

    public async Task UpdateContact()
    {

    }

    public async Task DeleteContact(Contact contact)
    {

    }

    public async Task<IReadOnlyCollection<Contact>> GetAllContacts()
        => await _contactRepo.GetAll();

    public async Task<Contact> GetContactDetails()
    {
        throw new NotImplementedException();
    }
}