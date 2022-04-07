using BestPlace.Core.Models.Category;

namespace BestPlace.Core.Contracts;

public interface ICategoryService
{
    Task<IEnumerable< CategoryListViewModel>> All();


    Task<bool> AddCategory(CategoryAddViewModel model);

    Task<CategoryEditViewModel> GetCategoryForEdit(Guid id);

    Task DeleteCategory(Guid id);

    Task EditCategory(CategoryEditViewModel model);

}