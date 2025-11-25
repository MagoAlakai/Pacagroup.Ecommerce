namespace Pacagroup.Ecommerce.Aplicacion.Interface.UseCases;

public interface IUserApplication
{
    Task<Response<bool>> CreateUserAsync(SignUpDTO user);
    Task<Response<TokenDTO>> IsValidUserAsync(SignInDTO user);
}
