using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.Country;

namespace PS.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;


    public CountryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;  
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _serviceManager.CountryService.GetAllAsync();
        return Ok(countries);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await _serviceManager.CountryService.GetByIdAsync(id);
        if (country == null) return NotFound();
        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCountryRequest createCountryRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await _serviceManager.CountryService.AddAsync(createCountryRequest);
        return CreatedAtAction(nameof(GetById), new { id = response.Data }, createCountryRequest);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryRequest updateCountryRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _serviceManager.CountryService.UpdateAsync(updateCountryRequest);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceManager.CountryService.DeleteAsync(id);
        return NoContent();
    }

}
