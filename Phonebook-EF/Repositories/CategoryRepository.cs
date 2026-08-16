using Microsoft.EntityFrameworkCore;
using Phonebook_EF;
public sealed class CategoryRepository : ICategoryRepository
{
    public async Task<IReadOnlyCollection<Category>> GetAll()
    {
        using (var db = new PhonebookContext())
        {
            var categories = await db.Categories.ToListAsync();
            return categories;
        }
    }

    public async Task<IReadOnlyCollection<Contact>> GetContactsByCategory(int categoryId)
    {
        using (var db = new PhonebookContext())
        {
            var contacts = await db.Contacts
                .Include(c => c.Category)
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
            return contacts;
        }
    }
}
