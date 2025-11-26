using Pacagroup.Ecommerce.Aplicacion.DTO.Identity;
using Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.CreateUserCommand;
using Pacagroup.Ecommerce.Aplicacion.Main.UseCases.User.Commands.IsValidUserCommand;

namespace Pacagroup.Ecommerce.Application.Test.User;

[TestClass]
public sealed class UserApplicationTest : LogicUnitTestAbstraction
{
    private readonly JsonSerializerOptions _json_serializer_options;
    public UserApplicationTest() => _json_serializer_options = new() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    private void ConsoleWriteObject(string prefix, object? obj)
    {
        string str_json = JsonSerializer.Serialize(obj, _json_serializer_options);
        Console.WriteLine($"{prefix}: {str_json}");
        _ = str_json;
        _ = prefix;
    }

    [TestMethod]
    public void CreateUserAsync_WithAllParameters_OkResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        CreateUserHandler createUserHandler = Scope.ServiceProvider.GetRequiredService<CreateUserHandler>();
        AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

        CreateUserCommand createUserCommand = new()
        {
            FirstName = "Mago",
            LastName = "Txukember",
            Email = "mago@gmail.com",
            Password = "Iloveswing",
            UserName = "magoalakai"
        };

        // Limpieza: borrar usuaria para no afectar a otros tests
        var userEntity = context.Users.SingleOrDefault(u => u.Email == createUserCommand.Email);
        if (userEntity is not null)
        {
            context.Users.Remove(userEntity);
            context.SaveChanges();
        }

        //Act
        Response<bool> response = createUserHandler.Handle(createUserCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
        Assert.AreEqual(response.Message, "User registered");
        Assert.IsNull(response.Errors);
    }

    [TestMethod]
    public void CreateUserAsync_UserAlreadyExist_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        CreateUserHandler createUserHandler = Scope.ServiceProvider.GetRequiredService<CreateUserHandler>();
        AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

        CreateUserCommand createUserCommand = new()
        {
            FirstName = "Mago",
            LastName = "Txukember",
            Email = "mago@gmail.com",
            Password = "Iloveswing",
            UserName = "magoalakai"
        };

        //Act
        Response<bool> responseInserted = createUserHandler.Handle(createUserCommand, CancellationToken.None).GetAwaiter().GetResult();
        Response<bool> response = createUserHandler.Handle(createUserCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
        Assert.AreEqual(response.Message, "This User already exists!");

        // Limpieza: borrar usuaria para no afectar a otros tests
        var userEntity = context.Users.SingleOrDefault(u => u.Email == createUserCommand.Email);
        if (userEntity is not null)
        {
            context.Users.Remove(userEntity);
            context.SaveChanges();
        }
    }

    [TestMethod]
    public void CreateUserAsync_WithNoEmail_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        CreateUserHandler createUserHandler = Scope.ServiceProvider.GetRequiredService<CreateUserHandler>();
        CreateUserCommand createUserCommand = new()
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = string.Empty,
            Password = "TestPassword",
            UserName = "TestUserName"
        };

        //Act
        Response<bool> response = createUserHandler.Handle(createUserCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
        Assert.AreEqual(response.Message, "Validation errors");
        Assert.IsTrue(response.Errors.Count() > 0);
    }

    [TestMethod]
    public void IsValidUserAsync_WithNoEmail_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        IsValidUserHandler isValidUserHandler = Scope.ServiceProvider.GetRequiredService<IsValidUserHandler>();
        IsValidUserCommand isValidUserCommand = new()
        {
            Email = string.Empty,
            Password = "TestPassword",
        };

        //Act
        Response<TokenDTO> response = isValidUserHandler.Handle(isValidUserCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Message, "Validation errors");
        Assert.IsTrue(response.Errors.Count() > 0);
    }

    [TestMethod]
    public void IsValidUserAsync_WithAllParameters_OkResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        IsValidUserHandler isValidUserHandler = Scope.ServiceProvider.GetRequiredService<IsValidUserHandler>();
        IsValidUserCommand isValidUserCommand = new()
        {
            Email = "mago@gmail.com",
            Password = "iloveswing"
        };

        //Act
        Response<TokenDTO> response = isValidUserHandler.Handle(isValidUserCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(response.Message, "Autenthication succesful");
        Assert.IsNull(response.Errors);
    }
}
