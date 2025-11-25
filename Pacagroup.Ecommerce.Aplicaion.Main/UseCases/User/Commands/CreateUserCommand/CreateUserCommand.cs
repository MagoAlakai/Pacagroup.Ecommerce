namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.CreateUserCommand;
public sealed record CreateUserCommand : IRequest<Response<bool>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
