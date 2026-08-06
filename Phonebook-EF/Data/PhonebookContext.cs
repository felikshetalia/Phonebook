using Microsoft.EntityFrameworkCore;
namespace Phonebook_EF;

public sealed class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public string dbPath { get; }

    public PhonebookContext()
    {
        Contacts = Set<Contact>();

        dbPath = Path.Combine(AppContext.BaseDirectory, "phonebook.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={dbPath}");

    public static void InitializeDatabase()
    {
        using var context = new PhonebookContext();
        SeedData.Initialize(context);
    }
}