using Microsoft.EntityFrameworkCore;
using Phonebook_EF;

public sealed class ContactRepository : IContactRepository
{
    public async Task Add(Contact contact)
    {
        using (var db = new PhonebookContext())
        {
            await db.Contacts.AddAsync(contact);
            await db.SaveChangesAsync();
        }
    }

    public async Task Delete(Contact contact)
    {
        using (var db = new PhonebookContext())
        {
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyCollection<Contact>> GetAll()
    {
        using (var db = new PhonebookContext())
        {
            Console.WriteLine($"REPO DB: {db.dbPath}");
            var contacts = await db.Contacts.ToListAsync();

            Console.WriteLine($"REPO FOUND: {contacts.Count} contacts");

            return contacts;
        }
    }

    public Task<Contact> GetOne(Contact contact)
    {
        throw new NotImplementedException();
    }

    public Task Update(int currentId, Contact newDetails)
    {
        throw new NotImplementedException();
    }
}