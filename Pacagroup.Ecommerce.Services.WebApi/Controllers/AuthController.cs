using Pacagroup.Ecommerce.Aplicacion.Validator;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
[SwaggerTag("Operaciones de Atenticación")]
public class AuthController(IUserApplication userApplication) : ControllerBase
{
    /// <summary>
    /// Register a new User
    /// </summary>
    /// <param name="signUpDTO"></param>
    /// <remarks>POST https://localhost:7087/api/auth/SignUp</remarks>
    /// <returns>Response<UserDTO></returns>

    [HttpPost("SignUp")]
    [SwaggerOperation(
    Summary = "Register a new User",
    Description = "Register User and return true")]
    [SwaggerResponse(200, "User registered", typeof(Response<TokenDTO>))]

    public async Task<IActionResult> SignUpAsync([FromBody] SignUpDTO signUpDTO)
    {
        try
        {
            Response<UserDTO?> response = await userApplication.CreateUserAsync(signUpDTO);

            if (response.IsSuccess is false || response.Data is null)
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Log in a User
    /// </summary>
    /// <param name="signInDTO"></param>
    /// <remarks>POST https://localhost:7087/api/auth/SignIn</remarks>
    /// <returns>Response<TokenDTO></returns>
    /// 
    [HttpPost("SignIn")]
    [SwaggerOperation(
    Summary = "Log in a User",
    Description = "Log in User and return an access token")]
    [SwaggerResponse(200, "User log in correctly", typeof(Response<TokenDTO>))]

    public async Task<IActionResult> SignInAsync([FromBody] SignInDTO signInDTO)
    {
        try
        {
            Response<TokenDTO> response = await userApplication.IsValidUserAsync(signInDTO);

            if (response.IsSuccess is false || response.Data is null)
                return Unauthorized(response);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
