namespace Pacagroup.Ecommerce.Aplicacion.DTO.Identity;

public sealed record TokenDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; }
}
