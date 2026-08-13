public class Category
{
    public int Id { get; set; }
    public required string Title { get; set; }

    IReadOnlyCollection<Contact> ContactsInCategory { get; set; } = [];
}
