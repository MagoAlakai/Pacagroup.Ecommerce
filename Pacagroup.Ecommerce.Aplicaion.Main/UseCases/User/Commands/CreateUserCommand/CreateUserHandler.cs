namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.CreateUserCommand;
public class CreateUserHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<CreateUserCommand> logger, SignUpDTOValidator signUpDTOValidator) : IRequestHandler<CreateUserCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Response<bool> response = new(); 
        SignUpDTO signUpDTO = mapper.Map<SignUpDTO>(request);
        ValidationResult? validation = await signUpDTOValidator.ValidateAsync(signUpDTO);

        if (validation.IsValid is false)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "Validation errors";
            response.Errors = validation.Errors;

            logger.LogError("Validation errors", response.Errors);

            return response;
        }

        string email = request.Email ?? throw new ArgumentNullException(nameof(request.Email));
        Domain.Entities.User? existingUser = await unitOfWork.userRepository.GetByEmailAsync(email);

        if (existingUser is not null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "This User already exists!";

            logger.LogError("User registration failed for email: {Email}", response.Message);

            return response;
        }

        Domain.Entities.User user = mapper.Map<Domain.Entities.User>(request);
        string password = request.Email ?? throw new ArgumentNullException(nameof(request.Password));
        bool data = await unitOfWork.userRepository.CreateUserAsync(user, password);

        if (data is false)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "Error inserting User";

            logger.LogError("User registration failed for email: {Email}", response.Message);

            return response;
        }

        await unitOfWork.SaveChangesAsync();
        Domain.Entities.User? existingUsernow = await unitOfWork.userRepository.GetByEmailAsync(email);
        UserDTO userDTOInserted = mapper.Map<UserDTO>(existingUsernow);

        response.IsSuccess = true;
        response.Message = "User registered";
        response.Data = true;

        return response;
    }
}
