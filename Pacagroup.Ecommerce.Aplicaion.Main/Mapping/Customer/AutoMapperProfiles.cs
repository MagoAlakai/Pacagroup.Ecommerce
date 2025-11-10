namespace Pacagroup.Ecommerce.Aplicacion.Main.Mapping.Customer;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Domain.Entity.Customer, CustomerDTO>().ReverseMap();
    }
}
