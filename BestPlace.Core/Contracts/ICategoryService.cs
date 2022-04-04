using BestPlace.Core.Models.Category;

namespace BestPlace.Core.Contracts;

public interface ICategoryService
{
    Task<IEnumerable< CategoryListViewModel>> All();

    Task<IEnumerable<CategorySelectListViewModel>> Selection();

    Task<bool> AddCategory(CategoryAddViewModel model);

    Task<CategoryEditViewModel> GetCategoryForEdit(Guid id);

    Task<bool> DeleteCategory(Guid id);

    Task<bool> EditCategory(CategoryEditViewModel model);

}