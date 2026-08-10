using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Phonebook_EF;

public sealed class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

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
        .LogTo(Console.WriteLine, LogLevel.Information)
        .UseSeeding((ctx, _) => SeedData.Initialize(ctx));

    public static void InitializeDatabase()
    {
        using var context = new PhonebookContext();
        Console.WriteLine($"Database path: {context.dbPath}");

        Console.WriteLine("Known migrations:");
        foreach (var migration in context.Database.GetMigrations())
        {
            Console.WriteLine($"  {migration}");
        }

        Console.WriteLine("Applied migrations BEFORE Migrate:");
        foreach (var migration in context.Database.GetAppliedMigrations())
        {
            Console.WriteLine($"  {migration}");
        }

        Console.WriteLine("Pending migrations BEFORE Migrate:");
        foreach (var migration in context.Database.GetPendingMigrations())
        {
            Console.WriteLine($"  {migration}");
        }

        context.Database.Migrate();

        Console.WriteLine("Applied migrations AFTER Migrate:");
        foreach (var migration in context.Database.GetAppliedMigrations())
        {
            Console.WriteLine($"  {migration}");
        }
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