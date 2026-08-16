public sealed class CategoriesService
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoriesService(ICategoryRepository repo)
    {
        _categoryRepo = repo;
    }

    public async Task<IReadOnlyCollection<Category>> GetAllCategories()
    {
        try
        {
            return await _categoryRepo.GetAll();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IReadOnlyCollection<Contact>> GetContactsByCategory(int categoryId)
    {
        try
        {
            return await _categoryRepo.GetContactsByCategory(categoryId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}