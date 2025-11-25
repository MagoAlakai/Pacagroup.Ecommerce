using Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.CreateUserCommand;
using Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.IsValidUserCommand;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
[SwaggerTag("Operaciones de Atenticación")]
public class AuthController(IUserApplication userApplication, SignUpDTOValidator signUpDTOValidator, SignInDTOValidator signInDTOValidator, IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Register a new User
    /// </summary>
    /// <param name="createUserCommand"></param>
    /// <remarks>POST https://localhost:7087/api/auth/SignUp</remarks>
    /// <returns>Response<UserDTO></returns>

    [HttpPost("SignUp")]
    [SwaggerOperation(
    Summary = "Register a new User",
    Description = "Register User and return true")]
    [SwaggerResponse(200, "User registered", typeof(Response<TokenDTO>))]

    public async Task<IActionResult> SignUpAsync([FromBody] CreateUserCommand createUserCommand)
    {
        //ValidationResult? validation = await signUpDTOValidator.ValidateAsync(signUpDTO);
        //if (validation.IsValid is false) return BadRequest();
        
        try
        {
            Response<bool> response = await mediator.Send(createUserCommand);

            if (response.IsSuccess is false || response.Data is false)
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

    public async Task<IActionResult> SignInAsync([FromBody] IsValidUserCommand isValidUserCommand)
    {
        //ValidationResult? validation = await signInDTOValidator.ValidateAsync(signInDTO);
        //if (validation.IsValid is false) return BadRequest();

        try
        {
            Response<TokenDTO> response = await mediator.Send(isValidUserCommand);

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
