using Microsoft.AspNetCore.Authorization;
using Pacagroup.Ecommerce.Aplicacion.DTO.Customer;
using Pacagroup.Ecommerce.Aplicacion.Interface.UseCases;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Authorize]
[Route("api/customer")]
[ApiController]
[SwaggerTag("Operaciones de Customer")]
public class CustomerController(ICustomerApplication customerApplication) : ControllerBase
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
            Response<IEnumerable<CustomerDTO>> response = await customerApplication.GetAllAsync();

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
        try
        {
            Response<CustomerDTO> response = await customerApplication.GetAsync(customerId);

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
    /// <returns></returns>

    [HttpPost("InsertAsync")]
    [SwaggerOperation(
    Summary = "Insert a Customer",
    Description = "Insert a Customer at the database")]
    [SwaggerResponse(200, "Insert successful", typeof(Response<CustomerDTO>))]
    public async Task<IActionResult> PostAsync(CustomerDTO customerDTO)
    {
        if (customerDTO is null) return BadRequest();

        try
        {
            Response<CustomerDTO> response = await customerApplication.InsertAsync(customerDTO);

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
    /// Update a Customer
    /// </summary>
    /// <param name="customerDTO"></param>
    /// /// <param name="customerId"></param>
    /// <remarks>PUT https://localhost:7087/api/customer/UpdateAsync/{customerId}</remarks>
    /// <returns></returns>

    [HttpPut("UpdateAsync/{customerId}")]
    [SwaggerOperation(
    Summary = "Update a Customer",
    Description = "Update a Customer at the database")]
    [SwaggerResponse(200, "Update successful", typeof(Response<bool>))]
    public async Task<IActionResult> UpdateAsync(CustomerDTO customerDTO, string customerId)
    {
        if (customerDTO is null || customerId is null || customerId.Equals(customerDTO.CustomerId) is false)
            return BadRequest();

        try
        {
            Response<bool> response = await customerApplication.UpdateAsync(customerDTO);

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
        try
        {
            Response<bool> response = await customerApplication.DeleteAsync(customerId);

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
