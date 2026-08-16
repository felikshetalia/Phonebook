namespace Phonebook_EF;

public sealed class CategoriesController
{
    private readonly ICategoriesView _categoriesView;
    private readonly CategoriesService _categoryService;

    public CategoriesController(ICategoriesView categoriesView, CategoriesService service)
    {
        _categoriesView = categoriesView;
        _categoryService = service;
    }

    public async Task Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            CategoriesMenuOption selectedOption = _categoriesView.DisplayCategoriesMenu();

            switch (selectedOption)
            {
                case CategoriesMenuOption.Family:
                    await DisplayContactsByCategory(1);
                    break;

                case CategoriesMenuOption.Friends:
                    await DisplayContactsByCategory(2);
                    break;

                case CategoriesMenuOption.Work:
                    await DisplayContactsByCategory(3);
                    break;

                case CategoriesMenuOption.Other:
                    await DisplayContactsByCategory(4);
                    break;

                case CategoriesMenuOption.Back:
                    isRunning = false;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(selectedOption),
                        selectedOption,
                        "Unknown menu option.");
            }
        }
    }

    private async Task DisplayContactsByCategory(int categoryId)
    {
        try
        {
            var contacts = await _categoryService.GetContactsByCategory(categoryId);

            if (contacts.Count == 0)
            {
                _categoriesView.DisplayMessage("No contacts found in this category.");
            }
            else
            {
                _categoriesView.DisplayContactsList(contacts);
            }

            _categoriesView.WaitForInput();
        }
        catch (Exception e)
        {
            _categoriesView.DisplayError(e.Message);
            _categoriesView.WaitForInput();
        }
    }

    public async Task<IReadOnlyCollection<Category>> FetchCategories()
    {
        IReadOnlyCollection<Category> categoryList;

        try
        {
            categoryList = await _categoryService.GetAllCategories();
            return categoryList;
        }
        catch (Exception e)
        {
            _categoriesView.DisplayError(e.Message);
        }
        return [];
    }
}