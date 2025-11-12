namespace Pacagroup.Ecommerce.Aplicacion.DTO.Identity;
public sealed record SignInDTO
{
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
}
