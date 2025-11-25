namespace Pacagroup.Ecommerce.Aplicacion.Main.Mapping.Customer;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CustomerDTO, Domain.Entities.Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Domain.Entities.Customer, CustomerDTO>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Domain.Entities.Customer, CreateCustomerCommand>()
           .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));

        CreateMap<CreateCustomerCommand, Domain.Entities.Customer>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<SignUpDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, IsValidUserCommand>().ReverseMap();
    }
}
