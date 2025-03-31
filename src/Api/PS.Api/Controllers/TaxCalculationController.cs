using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.TaxCalculation;
using PS.Shared.Exceptions;

namespace PS.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TaxCalculationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TaxCalculationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager; 
    }

    [HttpGet("fetch")]
    public async Task<IActionResult> FetchAsync()
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.TaxCalculationService.FindAll();
        if (!result.Successful)
        {
            if (result.Data == null || !result.Data.Any())
                return NoContent();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        
        return Ok(result.Data);
    }

    [HttpPost("taxregime")]
    public async Task<IActionResult> GetTaxRegimeAsync([FromBody] int countryId)
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.CountryService.GetByIdAsync(countryId);
        if (!result.Successful)
        {
            if (string.IsNullOrWhiteSpace(result.Data!.TaxRegime))
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(result.Data!.TaxRegime);
        //return new JsonResult(result.Data!.TaxRegime);
    }

    [HttpPost("taxregime/{countryId:int}")]
    public async Task<IActionResult> FetchTaxRegimeAsync([FromBody] int countryId)
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.CountryService.GetByIdAsync(countryId);
        if (!result.Successful)
        {
            if (string.IsNullOrWhiteSpace(result.Data!.TaxRegime))
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(result.Data!.TaxRegime);
        //return new JsonResult(result.Data!.TaxRegime);
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> CalculateTaxAsync([FromBody] bool calculate)
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.TaxCalculationService.CalculateTaxAsync(calculate);
        if (!result.Successful)
        {
            if (!result.Data!.Any())
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok(result.Data);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateTaxCalculationAsync([FromBody] UpdateTaxCalculationRequest updateTaxCalculationRequest)
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.TaxCalculationService.UpdateTaxCalculationAsync(updateTaxCalculationRequest);
        if (!result.Successful)
        {
            if (result.Data! <= 0)
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok(result.Data);
    }

    [HttpPut("adoupdate/{id:int}")]
    public async Task<IActionResult> ADOUpdateTaxCalculationAsync(int id, [FromBody] UpdateTaxCalculationRequest updateTaxCalculationRequest)
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.TaxCalculationService.ADOUpdateSingleTaxCalculationAsync(id, updateTaxCalculationRequest);
        if (!result.Successful)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
        return Ok(result.Data);
    }

    [HttpPost("run")]
    public async Task<IActionResult> RunTaxCalculation([FromBody] bool calculate)
    {
        //ToDo: FluentValidation
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _serviceManager.TaxCalculationService.RunTaxCalculation();
        return Ok(result.Data);
    }


}
