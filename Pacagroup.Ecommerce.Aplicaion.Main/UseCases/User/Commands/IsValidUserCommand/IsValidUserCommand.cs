namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.IsValidUserCommand;
public sealed record IsValidUserCommand : IRequest<Response<TokenDTO>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
