using Pacagroup.Ecommerce.Domain.Enum;

namespace Pacagroup.Ecommerce.Aplicacion.DTO.Discount;

public sealed record DiscountDTO
{
    public DateTime CreatedDate { get; set; }
    public int? Porcentage { get; set; }
    public DiscountStatus Status { get; set; }
}
