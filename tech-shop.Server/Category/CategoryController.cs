using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Category;

namespace shop.Server.Category
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(ICategoryCrud categoryCrud) : Controller
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() => Ok(await  categoryCrud.GetAll());

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await categoryCrud.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var categoryDto = await categoryCrud.Create(dto);

            return Ok(categoryDto);
        }
    }
}
