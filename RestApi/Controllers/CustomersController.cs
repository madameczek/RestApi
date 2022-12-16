using System.Data;
using Microsoft.AspNetCore.Mvc;
using RestApi.App.DataSource;
using RestApi.Resources;

namespace RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : Controller
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerService _customerService;

    public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerResource customerResource, CancellationToken ct = default)
    {
        try
        {
            var id = await _customerService.Create(customerResource, ct);
            _logger.LogInformation(
                "{EndpointName} endpoint invoked. Customer: {Id} has been created", nameof(Create), id);
            return Created($"/customers/{id}", null);
        }
        catch (ArgumentException e)
        {
            _logger.LogInformation(e, "{EndpointName} endpoint invoked. Invalid argument found", nameof(Create));
            return BadRequest($"{e.Message}");
        }
        catch (TaskCanceledException) { return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable); }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured when {EndpointName} endpoint was invoked", nameof(Create));
            return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable, "Internal application error");
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct = default)
    {
        try
        {
            return Ok(await _customerService.Get(id, ct));
        }
        catch (RowNotInTableException)
        {
            _logger.LogInformation("{EndpointName} endpoint invoked. Customer: {Id} was not found", nameof(Get), id);
            return NotFound($"Customer with id: {id} was not found");
        }
        catch (TaskCanceledException) { return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable); }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured when {EndpointName} endpoint was invoked", nameof(Get));
            return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable,"Internal application error");
        }
    }
    
     [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(string id, CustomerResource customerResource, CancellationToken ct = default)
    {
        try
        {
            await _customerService.Update(id, customerResource, ct);
            _logger.LogInformation(
                "{EndpointName} endpoint invoked. Customer: {Id} has been updated", nameof(Update), id);
            return NoContent();
        }
        catch (RowNotInTableException)
        {
            _logger.LogInformation(
                "{EndpointName} endpoint invoked. Customer: {Id} was not found", nameof(Update), id);
            return NotFound($"Customer with id: {id} was not found");
        }
        catch (ArgumentException e) when (e is ArgumentNullException)
        {
            _logger.LogInformation(e,"{EndpointName} endpoint invoked. Invalid argument found",nameof(Update));
            return BadRequest($"{e.Message}");
        }
        catch (TaskCanceledException) { return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable); }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured when {EndpointName} endpoint was invoked", nameof(Update));
            return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable,"Internal application error");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct = default)
    {
        try
        {
            await _customerService.Remove(id, ct);
            _logger.LogInformation("{EndpointName} endpoint invoked. Customer: {Id} has been deleted", nameof(Delete), id);
            return NoContent();
        }
        catch (RowNotInTableException)
        {
            _logger.LogInformation("{EndpointName} endpoint invoked. Customer: {Id} was not found", nameof(Delete), id);
            return NotFound($"Customer with id: {id} was not found");
        }
        catch (TaskCanceledException) { return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable); }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured when {EndpointName} endpoint was invoked", nameof(Delete));
            return StatusCode(statusCode: StatusCodes.Status503ServiceUnavailable,"Internal application error");
        }
    }
}