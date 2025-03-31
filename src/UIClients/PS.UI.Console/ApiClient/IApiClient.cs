using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.ApiClient;


public interface IApiClient
{
    Task<TResponse> GetAsync<TResponse>(string endpoint);
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
    Task<TResponse> PostAsync<TResponse>(string endpoint);
    Task<bool> PostBoolAsync<TRequest>(string endpoint, TRequest request);
    Task<int> PostIntAsync<TRequest>(string endpoint, TRequest request);
    Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);
    Task DeleteAsync(string endpoint);
}
