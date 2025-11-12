namespace Pacagroup.Ecommerce.Aplicacion.DTO.Identity;

public sealed record SignUpDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
