public interface ICategoryRepository
{
    Task<IReadOnlyCollection<Category>> GetAll();
    Task<IReadOnlyCollection<Contact>> GetContactsByCategory(int categoryId);
}