namespace Pacagroup.Ecommerce.Application.Test.User;

//[TestClass]
//public sealed class UserApplicationTest : LogicUnitTestAbstraction
//{
//    private readonly JsonSerializerOptions _json_serializer_options;
//    public UserApplicationTest() => _json_serializer_options = new() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
//    private void ConsoleWriteObject(string prefix, object? obj)
//    {
//        string str_json = JsonSerializer.Serialize(obj, _json_serializer_options);
//        Console.WriteLine($"{prefix}: {str_json}");
//        _ = str_json;
//        _ = prefix;
//    }

    //[TestMethod]
    //public void CreateUserAsync_WithAllParameters_OkResponse()
    //{
    //    //Arrange
    //    using IServiceScope Scope = ScopeFactory.CreateScope();
    //    IUserApplication userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
    //    AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

    //    SignUpDTO signUpDTO = new()
    //    {
    //        FirstName = "Mago",
    //        LastName = "Txukember",
    //        Email = "mago@gmail.com",
    //        Password = "Iloveswing",
    //        UserName = "magoalakai"
    //    };

    //    // Limpieza: borrar usuaria para no afectar a otros tests
    //    var userEntity = context.Users.SingleOrDefault(u => u.Email == signUpDTO.Email);
    //    if (userEntity is not null)
    //    {
    //        context.Users.Remove(userEntity);
    //        context.SaveChanges();
    //    }

    //    //Act
    //    Response<bool> response = userApplication.CreateUserAsync(signUpDTO).GetAwaiter().GetResult();
    //    ConsoleWriteObject("Response", response);

    //    //Assert
    //    Assert.IsTrue(response.IsSuccess);
    //    Assert.IsTrue(response.Data);
    //    Assert.AreEqual(response.Message, "User registered");
    //    Assert.IsNull(response.Errors);
    //}

    //[TestMethod]
    //public void CreateUserAsync_UserAlreadyExist_ErrorResponse()
    //{
    //    //Arrange
    //    using IServiceScope Scope = ScopeFactory.CreateScope();
    //    IUserApplication userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
    //    AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

    //    SignUpDTO signUpDTO = new()

    //    {
    //        FirstName = "Mago",
    //        LastName = "Txukember",
    //        Email = "mago@gmail.com",
    //        Password = "Iloveswing",
    //        UserName = "magoalakai"
    //    };

    //    //Act
    //    Response<bool> response = userApplication.CreateUserAsync(signUpDTO).GetAwaiter().GetResult();
    //    Response<bool> response1 = userApplication.CreateUserAsync(signUpDTO).GetAwaiter().GetResult();
    //    ConsoleWriteObject("Response", response1);

    //    //Assert
    //    Assert.IsFalse(response1.IsSuccess);
    //    Assert.IsNull(response1.Data);
    //    Assert.AreEqual(response1.Message, "This User already exists!");

    //    // Limpieza: borrar usuaria para no afectar a otros tests
    //    var userEntity = context.Users.SingleOrDefault(u => u.Email == signUpDTO.Email);
    //    if (userEntity is not null)
    //    {
    //        context.Users.Remove(userEntity);
    //        context.SaveChanges();
    //    }
    //}

    //[TestMethod]
    //public void CreateUserAsync_WithNoEmail_ErrorResponse()
    //{
    //    //Arrange
    //    using IServiceScope Scope = ScopeFactory.CreateScope();
    //    IUserApplication userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
    //    SignUpDTO signUpDTO = new()
    //    {
    //        FirstName = "TestFirstName",
    //        LastName = "TestLastName",
    //        Email = string.Empty,
    //        Password = "TestPassword",
    //        UserName = "TestUserName"
    //    };

    //    //Act
    //    Response<bool> response = userApplication.CreateUserAsync(signUpDTO).GetAwaiter().GetResult();
    //    ConsoleWriteObject("Response", response);

    //    //Assert
    //    Assert.IsFalse(response.IsSuccess);
    //    Assert.IsNull(response.Data);
    //    Assert.AreEqual(response.Message, "Validation errors");
    //    Assert.IsTrue(response.Errors.Count() > 0);
    //}

    //[TestMethod]
    //public void IsValidUserAsync_WithNoEmail_ErrorResponse()
    //{
    //    //Arrange
    //    using IServiceScope Scope = ScopeFactory.CreateScope();
    //    IUserApplication userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
    //    SignInDTO signInDTO = new()
    //    {
    //        Email = string.Empty,
    //        Password = "TestPassword",
    //    };

    //    //Act
    //    Response<TokenDTO> response = userApplication.IsValidUserAsync(signInDTO).GetAwaiter().GetResult();
    //    ConsoleWriteObject("Response", response);

    //    //Assert
    //    Assert.IsFalse(response.IsSuccess);
    //    Assert.IsNull(response.Data);
    //    Assert.AreEqual(response.Message, "Validation errors");
    //    Assert.IsTrue(response.Errors.Count() > 0);
    //}

    //[TestMethod]
    //public void IsValidUserAsync_WithAllParameters_OkResponse()
    //{
    //    //Arrange
    //    using IServiceScope Scope = ScopeFactory.CreateScope();
    //    var userApplication = Scope.ServiceProvider.GetRequiredService<IUserApplication>();
    //    SignInDTO signInDTO = new()
    //    {
    //        Email = "mago@gmail.com",
    //        Password = "Iloveswing"
    //    };

    //    //Act
    //    Response<TokenDTO> response = userApplication.IsValidUserAsync(signInDTO).GetAwaiter().GetResult();
    //    ConsoleWriteObject("Response", response);

    //    //Assert
    //    Assert.IsTrue(response.IsSuccess);
    //    Assert.IsNotNull(response.Data);
    //    Assert.AreEqual(response.Message, "Autenthication succesful");
    //    Assert.IsNull(response.Errors);
    //}
//}
