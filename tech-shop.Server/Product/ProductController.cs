using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Extensions;
using shop.Core.Product;

namespace shop.Server.Product
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductCrud productCrud) : Controller
    {
        [HttpGet("all")]
        public async Task<ActionResult> GetAll(PageRequest page) => Ok(await productCrud.GetAll(page));

        [HttpGet("{productId}")]
        public async Task<ActionResult> GetById(Guid productId)
        {
            ProductDto? dto = await productCrud.GetById(productId);
            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var productDto = await productCrud.Create(dto);
            return Ok(productDto);
        }
    }
}
