


#region Services and DI

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.UI.Console.ApiClient;
using PS.UI.Console.Configurations;
using PS.UI.Console.Dtos;
using PS.UI.Console.Services;
using System.Diagnostics;

var serviceCollection = new ServiceCollection();

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)  // Set base path to the current directory
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

serviceCollection.AddHttpClient("PS", client =>
{
    client.BaseAddress = new Uri(appSettings!.ApiBaseUrl!);
    client.Timeout = TimeSpan.FromSeconds(appSettings.TimeoutSeconds);

});

serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddScoped<IApiClient, ApiClient>();
serviceCollection.AddScoped<ITaxCalculationService, TaxCalculationService>();


var serviceProvider = serviceCollection.BuildServiceProvider();


var config = serviceProvider.GetRequiredService<IConfiguration>();
var apiClient = serviceProvider.GetRequiredService<IApiClient>();
var taxCalculationService = serviceProvider.GetRequiredService<ITaxCalculationService>();

#endregion



#region Tax Calculation

Console.WriteLine("***********************************************");
Console.WriteLine("*                   Welcome                   *");
Console.WriteLine("***********************************************");
Console.WriteLine("Calculating....");
Console.WriteLine();

var methods = new Methods(taxCalculationService);
bool calculate = true;

Stopwatch sw = Stopwatch.StartNew();

var taxCalculationResponse = await methods.ProcessTaxCalculationsAsync(calculate); // 6000 ms
//var taxCalculationResponse = await methods.RunTaxCalculation(calculate); // 5000 ms

sw.Stop();

Console.WriteLine($"{taxCalculationResponse} calculations completed in {sw.ElapsedMilliseconds} ms");
Console.ReadKey();

#endregion


public class Methods
{
    private readonly ITaxCalculationService _taxCalculationService;

    public Methods(ITaxCalculationService taxCalculationService)
    {
        _taxCalculationService = taxCalculationService;
    }

    public async Task<int> ProcessTaxCalculationsAsync(bool calculate)
    {
        try
        {
            var calculationResponse = await _taxCalculationService.CalculateTaxAsync(calculate);
            if (!calculationResponse.Any())
            {
                return 0;
            }

            UpdateTaxCalculationRequest updateTaxCalculationRequest = new()
            {
                TaxCalculations = calculationResponse
            };

            var updateResponse = await UpdateTaxCalculationAsync(updateTaxCalculationRequest);
            if (updateResponse <= 0)
            {
                return 0;
            }

            return updateResponse;
        }
        catch (Exception)
        {

            throw;
        }

    }


    public async Task<int> UpdateTaxCalculationAsync(UpdateTaxCalculationRequest updateTaxCalculationRequest)
    {
        try
        {
            return await _taxCalculationService.UpdateTaxCalculationAsync(updateTaxCalculationRequest);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int> RunTaxCalculation(bool calculate)
    {
        try
        {
            var response = await _taxCalculationService.RunTaxCalculation(calculate);
            return response;
        }
        catch (Exception)
        {

            throw;
        }
    }


}
