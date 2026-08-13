namespace Phonebook_EF;

public sealed class CategoriesController
{
    private readonly ICategoriesView _categoriesView;
    public CategoriesController(ICategoriesView categoriesView)
    {
        _categoriesView = categoriesView;
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
                    _categoriesView.WaitForInput();
                    break;

                case CategoriesMenuOption.Friends:
                    _categoriesView.WaitForInput();
                    break;

                case CategoriesMenuOption.Work:
                    _categoriesView.WaitForInput();
                    break;

                case CategoriesMenuOption.Other:
                    _categoriesView.WaitForInput();
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
}