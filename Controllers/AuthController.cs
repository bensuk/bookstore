using bookstore.Auth;
using bookstore.Auth.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace bookstore.Controllers
{
    [ApiController]
    //[Route("empty")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<BookstoreUser> userManager;
        private readonly IJwtTokenService jwtTokenService;

        public AuthController(UserManager<BookstoreUser> userManager, IJwtTokenService jwtTokenService)
        {
            this.userManager = userManager;
            this.jwtTokenService = jwtTokenService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await userManager.FindByNameAsync(registerUserDto.UserName);

            if (user != null)
                return BadRequest("Request invalid.");

            var newUser = new BookstoreUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };

            var createUserResult = await userManager.CreateAsync(newUser, registerUserDto.Password);

            if (!createUserResult.Succeeded)
                return BadRequest("Could not create a user.");

            await userManager.AddToRoleAsync(newUser, BookstoreRoles.User);

            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByNameAsync(loginUserDto.UserName);

            if (user == null)
                return BadRequest("Incorrect username or password");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!isPasswordValid)
                return BadRequest("Incorrect username or password");

            var roles = await userManager.GetRolesAsync(user);
            var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new SuccessfulLoginDto(accessToken));
        }



    }
}
