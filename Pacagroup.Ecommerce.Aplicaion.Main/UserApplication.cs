namespace Pacagroup.Ecommerce.Aplicacion.Main;

public class UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IAppLogger<UserApplication> logger, UserDTOValidator UserDTOValidator, SignUpDTOValidator SignUpDTOValidator, SignInDTOValidator SignInDTOValidator) : IUserApplication
{
    public async Task<Response<UserDTO?>> CreateUserAsync(SignUpDTO signUpDTO)
    {
        Response<UserDTO?> response = new();
        ValidationResult? validation = await SignUpDTOValidator.ValidateAsync(signUpDTO);

        if (validation.IsValid is false)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "Validation errors";
            response.Errors = validation.Errors;

            logger.LogError("Validation errors", response.Errors);

            return response;
        }

        User? existingUser = await unitOfWork.userRepository.GetByEmailAsync(signUpDTO.Email);

        if (existingUser is not null)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "This User already exists!";

            logger.LogError("User registration failed for email: {Email}", response.Message);

            return response;
        }

        User user = mapper.Map<User>(signUpDTO);
        bool data = await unitOfWork.userRepository.CreateUserAsync(user, signUpDTO.Password);

        if (data is false)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "Error inserting User";

            logger.LogError("User registration failed for email: {Email}", response.Message);

            return response;
        }

        await unitOfWork.SaveChangesAsync();
        User? existingUsernow = await unitOfWork.userRepository.GetByEmailAsync(signUpDTO.Email);
        UserDTO userDTOInserted = mapper.Map<UserDTO>(existingUsernow);

        response.IsSuccess = true;
        response.Message = "User registered";
        response.Data = userDTOInserted;

        return response;
    }

    public async Task<Response<TokenDTO>> IsValidUserAsync(SignInDTO signInDTO)
    {
        Response<TokenDTO> response = new();
        ValidationResult? validation = await SignInDTOValidator.ValidateAsync(signInDTO);

        if (validation.IsValid is false)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "Validation errors";
            response.Errors = validation.Errors;

            logger.LogError("Validation errors", response.Errors);

            return response;
        }

        User? user = await unitOfWork.userRepository.GetByEmailAsync(signInDTO.Email);

        if (user is null)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "This Email is not valid";

            logger.LogError("Authentication failed for email: {Email}", response.Message);

            return response;
        }

        bool userExist = await unitOfWork.userRepository.IsValidUserAsync(user, signInDTO.Password);

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
