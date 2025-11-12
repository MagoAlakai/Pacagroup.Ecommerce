namespace Pacagroup.Ecommerce.Aplicacion.DTO.Identity;

public sealed record UserDTO
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
}
