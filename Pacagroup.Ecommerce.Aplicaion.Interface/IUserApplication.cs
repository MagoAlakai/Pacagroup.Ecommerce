using Pacagroup.Ecommerce.Aplicacion.DTO.Identity;

namespace Pacagroup.Ecommerce.Aplicacion.Interface;

public interface IUserApplication
{
    Task<Response<UserDTO?>> CreateUserAsync(SignUpDTO user);
    Task<Response<TokenDTO>> IsValidUserAsync(SignInDTO user);
}
