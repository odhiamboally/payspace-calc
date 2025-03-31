using PS.Application.Dtos.Common;
using PS.Application.Dtos.TaxCalculation;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IServices;
public interface ITaxCalculationService
{
    Task<Response<List<TaxCalculationResponse>>> FindAll();
    Task<Response<List<TaxCalculationResponse>>> CalculateTaxAsync(bool calculate);
    Task<Response<int>> RunTaxCalculation();
    Task<Response<int>> UpdateTaxCalculationAsync(UpdateTaxCalculationRequest updateTaxCalculationRequest);
    Task<Response<bool>> ADOUpdateSingleTaxCalculationAsync(int id, UpdateTaxCalculationRequest updateTaxCalculationRequest);
}
