using Pacagroup.Ecommerce.Aplicacion.DTO.Customer;
using Pacagroup.Ecommerce.Aplicacion.DTO.Identity;
using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Aplicacion.Main.Mapping.Customer;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CustomerDTO, Domain.Entities.Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Domain.Entities.Customer, CustomerDTO>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));

        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<SignUpDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }
}
