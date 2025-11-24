namespace Pacagroup.Ecommerce.Domain.Entities;

public class Discount : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public int? Porcentage { get; set; }
    public DiscountStatus Status {get; set;}
}