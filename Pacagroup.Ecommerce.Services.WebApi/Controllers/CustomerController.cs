using Microsoft.AspNetCore.Http.HttpResults;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerController (ICustomerApplication customerApplication) : ControllerBase
{
    /// <summary>
    /// Get all Customers
    /// </summary>
    /// <param></param>
    /// <remarks>GET https://localhost:7087/api/customer/GetAllAsync</remarks>
    /// <returns>An IEnumerable<CustomerDTO></returns>
    [HttpGet("GetAllAsync")]
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
    /// 
    /// </summary>
    /// <param name="customerDTO"></param>
    /// /// <param name="customerId"></param>
    /// <remarks>PUT https://localhost:7087/api/customer/UpdateAsync/{customerId}</remarks>
    /// <returns></returns>
    [HttpPut("UpdateAsync/{customerId}")]
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
