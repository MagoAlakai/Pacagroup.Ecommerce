namespace Pacagroup.Ecommerce.Aplicacion.Interface.UseCases;

public interface IUserApplication
{
    Task<Response<UserDTO?>> CreateUserAsync(SignUpDTO user);
    Task<Response<TokenDTO>> IsValidUserAsync(SignInDTO user);
}
