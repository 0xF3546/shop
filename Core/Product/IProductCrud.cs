using shop.Core.Extensions;

namespace shop.Core.Product
{
    public interface IProductCrud
    {
        Task<PageResult<ProductDto>> GetAll(IPageable pageable);
        Task<ProductDto?> GetById(Guid Id);
        Task<ProductDto?> Create(CreateProductDto createProductDto);
    }
}
