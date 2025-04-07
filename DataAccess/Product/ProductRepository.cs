using Microsoft.EntityFrameworkCore;
using shop.Core.Extensions;
using shop.Core.Product;
using shop.DataAccess.Database;

namespace shop.DataAccess.Product
{
    internal class ProductRepository(AppDbContext dbContext) : IProductCrud
    {
        public async Task<ProductDto?> Create(CreateProductDto createProductDto)
        {
            Product product = new(createProductDto.Name, createProductDto.Description, createProductDto.Price);
            product.Id = Guid.NewGuid();
            dbContext.Add(product);
            dbContext.SaveChanges();
            return await GetById(product.Id);
        }

        public Task<PageResult<ProductDto>> GetAll(IPageable page)
        {
            var result = dbContext.Products
                .Select(x => x.GetDto())
                .OrderBy(x => x.Id)
                .PageResponse(page, p => p);

            return Task.FromResult(result);
        }

        public async Task<ProductDto?> GetById(Guid Id)
        {
            var result = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null) return null;
            return result.GetDto();
        }
    }
}
