using Microsoft.EntityFrameworkCore;

namespace Phonebook_EF;

public static class SeedData
{
    public static void Initialize(PhonebookContext context)
    {
        context.Database.EnsureCreated();

        if (context.Contacts.Any())
        {
            return;
        }

        var contacts = new List<Contact>
        {
            new()
            {
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@example.com",
                PhoneNumber = "555-0101"
            },
            new()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@example.com",
                PhoneNumber = "555-0102"
            },
            new()
            {
                FirstName = "Clara",
                LastName = "Nguyen",
                Email = "clara.nguyen@example.com",
                PhoneNumber = "555-0103"
            }
        };

        context.Contacts.AddRange(contacts);
        context.SaveChanges();
    }
}
