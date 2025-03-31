using Microsoft.Extensions.Configuration;
using PS.UI.Console.ApiClient;
using PS.UI.Console.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.Services;


internal sealed class TaxCalculationService : ITaxCalculationService
{
    private readonly IApiClient _apiClient;
    private readonly IConfiguration _config;

    public TaxCalculationService(IApiClient apiClient, IConfiguration config)
    {
        _apiClient = apiClient; 
        _config = config;
    }

    public async Task<string> GetTaxRegimeAsync(int countryId)
    {
        return await _apiClient.PostAsync<int, string>(_config["AppSettings:EndPoints:PS:Tax:TaxRegime"]!, countryId);
    }

    public async Task<string> FetchTaxRegimeAsync(int countryId)
    {
        var url = $"{_config["AppSettings:EndPoints:PS:Tax:TaxRegime"]}/{countryId}";
        return await _apiClient.PostAsync<string>(url);
    }


    public async Task<List<TaxCalculationResponse>> CalculateTaxAsync(bool calculate)
    {
        return await _apiClient.PostAsync<bool, List<TaxCalculationResponse>>(_config["AppSettings:EndPoints:PS:Tax:Calculate"]!, calculate);
    }

    public async Task<bool> DeleteTaxCalculationAsync(int id, UpdateTaxCalculationRequest updateTaxCalculationRequest)
    {
        await _apiClient.DeleteAsync($"tax-calculations/{id}");
        return true;
    }

    public async Task<List<TaxCalculationResponse>> GetAllTaxCalculationsAsync()
    {
        return await _apiClient.GetAsync<List<TaxCalculationResponse>>(_config["AppSettings:EndPoints:PS:Tax:GetAll"]!);
    }

    public async Task<int> UpdateTaxCalculationAsync(UpdateTaxCalculationRequest updateTaxCalculationRequest)
    {
        return await _apiClient.PutAsync<UpdateTaxCalculationRequest, int>($"{_config["AppSettings:EndPoints:PS:Tax:Update"]}", updateTaxCalculationRequest);
    }

    public async Task<int> RunTaxCalculation(bool calculate)
    {
        var response = await _apiClient.PostIntAsync(_config["AppSettings:EndPoints:PS:Tax:Run"]!, calculate);
        return response;
    }


}
