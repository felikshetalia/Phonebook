using Microsoft.EntityFrameworkCore;

namespace Phonebook_EF;

public static class SeedData
{
    public static void Initialize(DbContext context)
    {

        if (!context.Set<Category>().Any())
        {
            var categories = new List<Category>
            {
                new() { Title = "Family" },
                new() { Title = "Friends" },
                new() { Title = "Work" },
                new() { Title = "Other" }
            };

            context.Set<Category>().AddRange(categories);
            context.SaveChanges();
        }

        if (!context.Set<Contact>().Any())
        {
            var contacts = new List<Contact>
            {
                new()
                {
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    PhoneNumber = "555-0101",
                    CategoryId = 1
                },
                new()
                {
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bob.smith@example.com",
                    PhoneNumber = "555-0102",
                    CategoryId = 3
                },
                new()
                {
                    FirstName = "Clara",
                    LastName = "Nguyen",
                    Email = "clara.nguyen@example.com",
                    PhoneNumber = "555-0103",
                    CategoryId = 2
                }
            };

            context.Set<Contact>().AddRange(contacts);
            context.SaveChanges();
        }

    }
}
