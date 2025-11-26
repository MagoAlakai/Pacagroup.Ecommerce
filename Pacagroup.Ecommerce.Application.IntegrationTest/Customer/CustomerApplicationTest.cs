
using Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Queries.GetCustomerQuery;

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
public void GetAllCustomersAsync_WithCustomers_OkResponse()
{
    //Arrange
    using IServiceScope Scope = ScopeFactory.CreateScope();
    GetAllCustomersHandler getAllCustomersHandler = Scope.ServiceProvider.GetRequiredService<GetAllCustomersHandler>();
    GetAllCustomersQuery getAllCustomersQuery = new();

    //Act
    Response<IEnumerable<CustomerDTO>> response = getAllCustomersHandler.Handle(getAllCustomersQuery, CancellationToken.None).GetAwaiter().GetResult();
    ConsoleWriteObject("Response", response);

    //Assert
    Assert.IsTrue(response.IsSuccess);
    Assert.IsNotNull(response.Data);
    Assert.AreEqual(response.Message, "Get All succesfull");
    Assert.IsNull(response.Errors);
}

    [TestMethod]
    public void GetCustomerAsync_CustomerDoesNotExist_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        GetCustomerHandler getAllCustomersHandler = Scope.ServiceProvider.GetRequiredService<GetCustomerHandler>();
        string customerId = "nonexistent-id";
        GetCustomerQuery getCustomerQuery = new() { CustomerId = customerId };

        //Act
        Response<CustomerDTO>? response = getAllCustomersHandler.Handle(getCustomerQuery, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Message, "This customer doesn't exist");
    }

    [TestMethod]
    public void GetCustomerAsync_WithCustomerId_OkResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        GetCustomerHandler getCustomerHandler = Scope.ServiceProvider.GetRequiredService<GetCustomerHandler>();
        string customerId = "ALFKI";
        GetCustomerQuery getCustomerQuery = new() { CustomerId = customerId };

        //Act
        Response<CustomerDTO>? response = getCustomerHandler.Handle(getCustomerQuery, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(response.Message, "Get succesfull");
        Assert.IsNull(response.Errors);
    }

    [TestMethod]
    public void CreateCustomerAsync_WithNoCustomerId_ErrorResponse()
    {
        //Arrange
        using IServiceScope Scope = ScopeFactory.CreateScope();
        CreateCustomerHandler createCustomerHandler = Scope.ServiceProvider.GetRequiredService<CreateCustomerHandler>();
        CreateCustomerCommand createCustomerCommand = new()
        {
            CustomerId = string.Empty,
            CompanyName = "TestCompanyName",
            ContactName = "TestContactName",
            ContactTitle = "TestContactTitle",
            Address = "TestAddress",
            City = "TestCity",
            Region = "TestRegion",
            PostalCode = "TestPostalCode",
            Country = "TestCountry",
            Phone = "TestPhone",
            Fax = "TestFax"
        };

        //Act
        Response<bool> response = createCustomerHandler.Handle(createCustomerCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsFalse(response.Data);
        Assert.AreEqual(response.Message, "Validation errors");
        Assert.IsTrue(response.Errors.Count() > 0);
    }

    [TestMethod]
    public void CreateCustomerAsync_WithCreateCustomerCommand_OkResponse()
    {
        //Arrange
        const string testId = "TEST1";
        using IServiceScope cleanupScope = ScopeFactory.CreateScope();
        CreateCustomerHandler createCustomerHandler = cleanupScope.ServiceProvider.GetRequiredService<CreateCustomerHandler>();
        AppDbContext context = cleanupScope.ServiceProvider.GetRequiredService<AppDbContext>();

        CreateCustomerCommand createCustomerCommand = new()
        {
            CustomerId = "TEST1",
            CompanyName = "TestCompanyName",
            ContactName = "TestContactName",
            ContactTitle = "TestContactTitle",
            Address = "TestAddress",
            City = "TestCity",
            Region = "TestRegion",
            PostalCode = "TestPC",
            Country = "TestCountry",
            Phone = "TestPhone",
            Fax = "TestFax"
        };

        Domain.Entities.Customer? existing = context.Customers.SingleOrDefault(c => c.Id == testId);

        if (existing is not null)
        {
            context.Customers.Remove(existing);
            context.SaveChangesAsync();
        }

        //Act
        Response<bool> response = createCustomerHandler.Handle(createCustomerCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsTrue(response.Data);
        Assert.AreEqual(response.Message, "Insert successful");
        Assert.IsNull(response.Errors);

        // Limpieza: borrar Customer para no afectar a otros tests
        Domain.Entities.Customer? created = context.Customers.SingleOrDefault(c => c.Id == testId);

        if (created is not null)
        {
            context.Customers.Remove(created);
            context.SaveChangesAsync();
        }
    }

    [TestMethod]
    public void CreateCustomerAsync_WithCustomerAlreadyCreated_ErrorResponse()
    {
        //Arrange
        const string testId = "TEST1";
        using IServiceScope Scope = ScopeFactory.CreateScope();
        CreateCustomerHandler createCustomerHandler = Scope.ServiceProvider.GetRequiredService<CreateCustomerHandler>();
        AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

        CreateCustomerCommand createCustomerCommand = new()
        {
            CustomerId = "TEST1",
            CompanyName = "TestCompanyName",
            ContactName = "TestContactName",
            ContactTitle = "TestContactTitle",
            Address = "TestAddress",
            City = "TestCity",
            Region = "TestRegion",
            PostalCode = "TestPC",
            Country = "TestCountry",
            Phone = "TestPhone",
            Fax = "TestFax"
        };

        Response<bool> response = createCustomerHandler.Handle(createCustomerCommand, CancellationToken.None).GetAwaiter().GetResult();

        //Act
        Response<bool> responseInserted = createCustomerHandler.Handle(createCustomerCommand, CancellationToken.None).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", responseInserted);

        //Assert
        Assert.IsFalse(responseInserted.IsSuccess);
        Assert.IsFalse(responseInserted.Data);
        Assert.AreEqual(responseInserted.Message, "This Customer already exists!");
        Assert.IsNull(responseInserted.Errors);

        // Limpieza: borrar Customer para no afectar a otros tests
        Domain.Entities.Customer? created = context.Customers.SingleOrDefault(c => c.Id == testId);

        if (created is not null)
        {
            context.Customers.Remove(created);
            context.SaveChangesAsync();
        }
    }
}
