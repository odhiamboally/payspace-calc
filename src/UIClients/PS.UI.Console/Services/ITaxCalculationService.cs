using PS.UI.Console.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.Services;


public interface ITaxCalculationService
{
    Task<string> GetTaxRegimeAsync(int countryId);
    Task<List<TaxCalculationResponse>> GetAllTaxCalculationsAsync();
    Task<List<TaxCalculationResponse>> CalculateTaxAsync(bool calculate);
    Task<int> UpdateTaxCalculationAsync(UpdateTaxCalculationRequest updateTaxCalculationRequest);
    Task<int> RunTaxCalculation(bool calculate);
}
