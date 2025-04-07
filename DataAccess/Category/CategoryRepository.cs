using Microsoft.EntityFrameworkCore;
using shop.Core.Category;
using shop.DataAccess.Database;

namespace shop.DataAccess.Category
{
    internal class CategoryRepository(AppDbContext dbContext) : ICategoryCrud
    {
        public Task<CategoryDto> Create(CreateCategoryDto createCategoryDto)
        {
            Category category = new(createCategoryDto.Name);
            category.Id = Guid.NewGuid();
            dbContext.Add(category);
            dbContext.SaveChanges();

            return GetById(category.Id);
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            return await dbContext.Categories.Select(x => x.GetDto()).ToListAsync();
        }

        public Task<CategoryDto?> GetById(Guid id)
        {
            var dto = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (dto == null) return null;
            return Task.FromResult(dto.GetDto());
        }
    }
}
