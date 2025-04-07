namespace shop.Core.Category
{
    public interface ICategoryCrud
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto?> GetById(Guid id);
        Task<CategoryDto> Create(CreateCategoryDto createCategoryDto);
    }
}
