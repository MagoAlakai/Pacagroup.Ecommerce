namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.IsValidUserCommand;
public class IsValidUserHandler(IMapper mapper, IUnitOfWork unitOfWork, IJwtService jwtService, IAppLogger<IsValidUserCommand> logger, SignInDTOValidator signInDTOValidator) : IRequestHandler<IsValidUserCommand, Response<TokenDTO>>
{
    public async Task<Response<TokenDTO>> Handle(IsValidUserCommand request, CancellationToken cancellationToken)
    {
        Response<TokenDTO> response = new();
        string email = request.Email ?? throw new ArgumentNullException(nameof(request.Email));
        string password = request.Password ?? throw new ArgumentNullException(nameof(request.Password));

        SignInDTO signInDTO = mapper.Map<SignInDTO>(request);
        ValidationResult? validation = await signInDTOValidator.ValidateAsync(signInDTO);

        if (validation.IsValid is false)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "Validation errors";
            response.Errors = validation.Errors;

            logger.LogError("Validation errors", response.Errors);

            return response;
        }

        Domain.Entities.User? user = await unitOfWork.userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "This Email is not valid";

            logger.LogError("Authentication failed for email: {Email}", response.Message);

            return response;
        }

        bool userExist = await unitOfWork.userRepository.IsValidUserAsync(user, password);

        if (userExist is false)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "This User is not valid";

            logger.LogError("Authentication failed for email: {Email}", response.Message);

            return response;
        }

        string tokenDTO = jwtService.GenerateToken(user);
        response.Data = new TokenDTO
        {
            AccessToken = tokenDTO,
            TokenType = "Bearer",
            ExpiresIn = 3600
        };
        response.IsSuccess = true;
        response.Message = "Autenthication succesful";

        return response;
    }
}
