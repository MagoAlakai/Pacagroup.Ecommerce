using AutoMapper;
using Pacagroup.Ecommerce.Aplicacion.DTO.Customer;
using Pacagroup.Ecommerce.Aplicacion.DTO.Identity;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructura.Data;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();

        //Act
        Response<IEnumerable<CustomerDTO>> response = customerApplication.GetAllAsync().GetAwaiter().GetResult();
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
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();
        string customerId = "nonexistent-id";

        //Act
        Response<CustomerDTO>? response = customerApplication.GetAsync(customerId).GetAwaiter().GetResult();
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
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();
        string customerId = "ALFKI";

        //Act
        Response<CustomerDTO>? response = customerApplication.GetAsync(customerId).GetAwaiter().GetResult();
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
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();
        CustomerDTO customerDTO = new()
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
        Response<CustomerDTO>? response = customerApplication.InsertAsync(customerDTO).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsFalse(response.IsSuccess);
        Assert.IsNull(response.Data);
        Assert.AreEqual(response.Message, "Validation errors");
        Assert.IsTrue(response.Errors.Count() > 0);
    }

    [TestMethod]
    public void CreateCustomerAsync_WithCustomerDTO_OkResponse()
    {
        //Arrange
        const string testId = "TEST1";
        using IServiceScope cleanupScope = ScopeFactory.CreateScope();
        ICustomerApplication customerApplication = cleanupScope.ServiceProvider.GetRequiredService<ICustomerApplication>();
        AppDbContext context = cleanupScope.ServiceProvider.GetRequiredService<AppDbContext>();

        CustomerDTO customerDTO = new()
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

        var existing = context.Customers.SingleOrDefault(c => c.Id == testId);

        if (existing is not null)
        {
            context.Customers.Remove(existing);
            context.SaveChangesAsync();
        }

        //Act
        Response<CustomerDTO>? response = customerApplication.InsertAsync(customerDTO).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", response);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(response.Message, "Insert successful");
        Assert.IsNull(response.Errors);

        // Limpieza: borrar Customer para no afectar a otros tests
        Domain.Entity.Customer created = context.Customers.SingleOrDefault(c => c.Id == testId);

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
        ICustomerApplication customerApplication = Scope.ServiceProvider.GetRequiredService<ICustomerApplication>();
        AppDbContext context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

        CustomerDTO customerDTO = new()
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

        Response<CustomerDTO>? response = customerApplication.InsertAsync(customerDTO).GetAwaiter().GetResult();

        //Act
        Response<CustomerDTO>? responseInserted = customerApplication.InsertAsync(customerDTO).GetAwaiter().GetResult();
        ConsoleWriteObject("Response", responseInserted);

        //Assert
        Assert.IsFalse(responseInserted.IsSuccess);
        Assert.IsNull(responseInserted.Data);
        Assert.AreEqual(responseInserted.Message, "This Customer already exists!");
        Assert.IsNull(responseInserted.Errors);

        // Limpieza: borrar Customer para no afectar a otros tests
        Domain.Entity.Customer created = context.Customers.SingleOrDefault(c => c.Id == testId);

        if (created is not null)
        {
            context.Customers.Remove(created);
            context.SaveChangesAsync();
        }
    }
}
