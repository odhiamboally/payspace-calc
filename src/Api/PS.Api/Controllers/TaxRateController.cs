using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Dtos.TaxRate;

namespace PS.Api.Controllers;



[Route("api/[controller]")]
[ApiController]
public class TaxRateController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TaxRateController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }



    [HttpGet("country/{countryId:int}")]
    public async Task<IActionResult> GetByCountryId(int countryId)
    {
        var taxRates = await _serviceManager.TaxRateService.GetByCountryIdAsync(countryId);
        return Ok(taxRates);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaxRateRequest createTaxRateRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _serviceManager.TaxRateService.CreateAsync(createTaxRateRequest);
        return Created(string.Empty, createTaxRateRequest);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaxRateRequest updateTaxRateRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _serviceManager.TaxRateService.UpdateAsync(id, updateTaxRateRequest);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceManager.TaxRateService.DeleteAsync(id);
        return NoContent();
    }
}
