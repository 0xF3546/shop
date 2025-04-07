using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shop.Core.User;
using shop.DataAccess.User;

namespace shop.Server.User
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(SignInManager<ShopUser> signInManager, UserManager<ShopUser> userManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound();

            var result = await signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            if (!result.Succeeded)
                return Unauthorized();

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ShopUser { UserName = $"{dto.Firstname} ${dto.Lastname}", Email = dto.Email, Firstname = dto.Firstname, Lastname = dto.Lastname };
            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await signInManager.SignInAsync(user, false);
            return Ok();
        }
    }
}
