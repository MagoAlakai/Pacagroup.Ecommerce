namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Authorize]
[Route("api/customer")]
[ApiController]
[SwaggerTag("Operaciones de Customer")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get all Customers
    /// </summary>
    /// <param></param>
    /// <remarks>GET https://localhost:7087/api/customer/GetAllAsync</remarks>
    /// <returns>An IEnumerable<CustomerDTO></returns>
    
    [HttpGet("GetAllAsync")]
    [SwaggerOperation(
    Summary = "Get a Customer List",
    Description = "Return ageneric object List with the response of the request")]
    [SwaggerResponse(200, "Get All succesfull", typeof(Response<IEnumerable<CustomerDTO>>))]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            Response<IEnumerable<CustomerDTO>> response = await mediator.Send(new GetAllCustomersQuery());

            if (response.IsSuccess is false || response.Data is null)
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Get a Customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <remarks>GET https://localhost:7087/api/customer/GetAsync/{customerId}</remarks>
    /// <returns>A CustomerDTO</returns>

    [HttpGet("GetAsync/{customerId}")]
    [SwaggerOperation(
    Summary = "Get a Customer",
    Description = "Return a generic object with the response of the request")]
    [SwaggerResponse(200, "Get succesfull", typeof(Response<CustomerDTO>))]
    public async Task<IActionResult> GetAsync(string customerId)
    {
        if (customerId is null) return BadRequest(nameof(customerId));

        try
        {
            Response<CustomerDTO> response = await mediator.Send(new GetCustomerQuery(){ CustomerId = customerId });

            if (response.IsSuccess is false || response.Data is null)
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Create a Customer
    /// </summary>
    /// <param name="customerDTO"></param>
    /// <remarks>POST https://localhost:7087/api/customer/InsertAsync</remarks>
    /// <returns> Response<bool> </returns>

    [HttpPost("InsertAsync")]
    [SwaggerOperation(
    Summary = "Insert a Customer",
    Description = "Insert a Customer at the database")]
    [SwaggerResponse(200, "Insert successful", typeof(Response<CustomerDTO>))]
    public async Task<IActionResult> PostAsync(CreateCustomerCommand createCustomercommand)
    {
        try
        {
            Response<bool> response = await mediator.Send(createCustomercommand);

            if (response.IsSuccess is false || response.Data is false)
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception e)
        { 
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Update a Customer
    /// </summary>
    /// <param name="updateCustomerCommand"></param>
    /// /// <param name="customerId"></param>
    /// <remarks>PUT https://localhost:7087/api/customer/UpdateAsync/{customerId}</remarks>
    /// <returns></returns>

    [HttpPut("UpdateAsync/{customerId}")]
    [SwaggerOperation(
    Summary = "Update a Customer",
    Description = "Update a Customer at the database")]
    [SwaggerResponse(200, "Update successful", typeof(Response<bool>))]
    public async Task<IActionResult> UpdateAsync(UpdateCustomerCommand updateCustomerCommand, string customerId)
    {
        try
        {
            Response<bool> response = await mediator.Send(updateCustomerCommand);

            if (response.IsSuccess is false || response.Data is false)
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    /// <summary>
    /// Delete a Customer
    /// </summary>
    /// <remarks>DELETE https://localhost:7087/api/customer/DeleteAsync/{customerId}</remarks>
    /// <param name="customerId"></param>
    /// <returns></returns>

    [HttpDelete("DeleteAsync/{customerId}")]
    [SwaggerOperation(
    Summary = "Delete a Customer",
    Description = "Delete a Customer at the database")]
    [SwaggerResponse(200, "Delete successful", typeof(Response<bool>))]
    public async Task<IActionResult> DeleteAsync(string customerId)
    {
        if (customerId is null) return BadRequest(nameof(customerId));

        try
        {
            Response<bool> response = await mediator.Send(new DeleteCustomerCommand() { CustomerId = customerId });

            if (response.IsSuccess is false || response.Data is false)
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
