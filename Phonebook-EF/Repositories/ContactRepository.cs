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

    public async Task Delete(int contactId)
    {
        using (var db = new PhonebookContext())
        {
            var contact = await db.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);

            if (contact != null)
            {
                db.Contacts.Remove(contact);
                await db.SaveChangesAsync();
            }
            else throw new Exception("Item with a given id couldn't be found");
        }
    }

    public async Task<IReadOnlyCollection<Contact>> GetAll()
    {
        using (var db = new PhonebookContext())
        {
            var contacts = await db.Contacts.Include(c => c.Category).ToListAsync();
            return contacts;
        }
    }

    public async Task<Contact> GetOne(int contactId)
    {
        using (var db = new PhonebookContext())
        {
            var contact = await db.Contacts.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == contactId);

            if (contact != null)
            {
                return contact;
            }
            else throw new Exception("Item with a given id couldn't be found");
        }
    }

    public async Task Update(int currentId, Contact newDetails)
    {
        using (var db = new PhonebookContext())
        {
            var contact = await db.Contacts.FirstOrDefaultAsync(c => c.Id == currentId);

            if (contact != null)
            {
                contact.FirstName = newDetails.FirstName;
                contact.LastName = newDetails.LastName;
                contact.Email = newDetails.Email;
                contact.PhoneNumber = newDetails.PhoneNumber;
                await db.SaveChangesAsync();
            }
            else throw new Exception("Item with a given id couldn't be found");
        }
    }
}