using Microsoft.EntityFrameworkCore;
namespace Phonebook_EF;

public sealed class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    public string dbPath { get; }

    public PhonebookContext()
    {

        var projectDir = GetProjectDirectory();
        var dataDir = Path.Combine(projectDir, "Data");

        Directory.CreateDirectory(dataDir);

        dbPath = Path.Combine(dataDir, "phonebook.db");
        Contacts = Set<Contact>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={dbPath}")
        .UseSeeding((ctx, _) => SeedData.Initialize(ctx));

    public static void InitializeDatabase()
    {
        using var context = new PhonebookContext();

        context.Database.Migrate();
    }

    private static string GetProjectDirectory()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);

        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "Phonebook-EF.csproj")))
                return dir.FullName;

            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate Phonebook-EF project directory.");
    }
}