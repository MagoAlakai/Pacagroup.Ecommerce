using Pacagroup.Ecommerce.Aplicacion.DTO.Customer;
using Pacagroup.Ecommerce.Aplicacion.DTO.Identity;

namespace Pacagroup.Ecommerce.Aplicacion.Main.Mapping.Customer;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CustomerDTO, Domain.Entity.Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Domain.Entity.Customer, CustomerDTO>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Domain.Entity.User, UserDTO>().ReverseMap();

        CreateMap<SignUpDTO, Domain.Entity.User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }
}
