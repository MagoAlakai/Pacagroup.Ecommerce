using Pacagroup.Ecommerce.Aplicacion.DTO.Customer;

namespace Pacagroup.Ecommerce.Application.Test.Customer;

[TestClass]
public sealed class CustomerApplicationTest : LogicUnitTestAbstraction
{
    private readonly JsonSerializerOptions _json_serializer_options;
    public CustomerApplicationTest() => _json_serializer_options = new() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    private void ConsoleWriteObject(string prefix, object? obj)
    {
        string str_json = JsonSerializer.Serialize(obj, _json_serializer_options);
        Console.WriteLine($"{prefix}: {str_json}");
        _ = str_json;
        _ = prefix;
    }

    [TestMethod]
    public void GetAllUsersAsync_WithNoCustomer_InfoResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();

        //Act
        Response<IEnumerable<CustomerDTO>> response = customerApplication.GetAllAsync().GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Message, "There are no costumers");
    }

    [TestMethod]
    public void GetAllUsersAsync_WithCustomers_OkResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();

        //Act
        Response<IEnumerable<CustomerDTO>> response = customerApplication.GetAllAsync().GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(response.Message, "Get All succesfull");
    }

    [TestMethod]
    public void CreateUserAsync_UserAlreadyExist_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        var userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
        SignUpDTO signUpDTO = new()
        {
            FirstName = "Mago",
            LastName = "Txukember",
            Email = "mago@gmail.com",
            Password = "Iloveswing",
            UserName = "magoalakai"
        };

        //Act
        Response<UserDTO?> response = userApplication.CreateUserAsync(signUpDTO).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Message, "This User already exists!");
    }

    [TestMethod]
    public void CreateUserAsync_WithNoEmail_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        var userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
        SignUpDTO signUpDTO = new()
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = string.Empty,
            Password = "TestPassword",
            UserName = "TestUserName"
        };

        //Act
        Response<UserDTO?> response = userApplication.CreateUserAsync(signUpDTO).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Message, "Validation errors");
        Assert.IsTrue(response.Errors.Count() > 0);
    }

    [TestMethod]
    public void IsValidUserAsync_WithNoEmail_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        var userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
        SignInDTO signInDTO = new()
        {
            Email = string.Empty,
            Password = "TestPassword",
        };

        //Act
        Response<TokenDTO> response = userApplication.IsValidUserAsync(signInDTO).GetAwaiter().GetResult();
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
        var userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
        SignInDTO signInDTO = new()
        {
            Email = "mago@gmail.com",
            Password = "Iloveswing"
        };

        //Act
        Response<TokenDTO> response = userApplication.IsValidUserAsync(signInDTO).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(response.Message, "Autenthication succesful");
    }
}
