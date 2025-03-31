using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.TaxBracket;

namespace PS.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TaxBracketController : ControllerBase
{
    private readonly IServiceManager _serviceManager;


    public TaxBracketController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;  
    }

    [HttpGet("country/{countryId:int}")]
    public async Task<IActionResult> GetByCountryId(int countryId)
    {
        var taxBrackets = await _serviceManager.TaxBracketService.GetByCountryAsync(countryId);
        return Ok(taxBrackets);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaxBracketRequest createTaxBracketRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _serviceManager.TaxBracketService.AddAsync(createTaxBracketRequest);
        return Created(string.Empty, createTaxBracketRequest);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaxBracketRequest updateTaxBracketRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _serviceManager.TaxBracketService.UpdateAsync(updateTaxBracketRequest);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceManager.TaxBracketService.DeleteAsync(id);
        return NoContent();
    }


}
