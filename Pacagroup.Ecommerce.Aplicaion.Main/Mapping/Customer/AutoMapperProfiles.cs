namespace Pacagroup.Ecommerce.Aplicacion.Main.Mapping.Customer;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //CreateMap<Domain.Entity.Customer, CustomerDTO>().ReverseMap();

        CreateMap<CustomerDTO, Domain.Entity.Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Domain.Entity.Customer, CustomerDTO>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));
    }
}
