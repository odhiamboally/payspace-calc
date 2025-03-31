using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.ApiClient;


internal sealed class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("PS");
    }

    public async Task DeleteAsync(string endpoint)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<TResponse> GetAsync<TResponse>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentLength == 0)
            {
                if (typeof(TResponse).IsValueType)
                    return default!;

                return default!;
            }

            if (typeof(TResponse) == typeof(string) || typeof(TResponse) == typeof(int) || typeof(TResponse) == typeof(decimal))
            {
                var plainText = await response.Content.ReadAsStringAsync();
                return (TResponse)Convert.ChangeType(plainText, typeof(TResponse));
            }

            return await response.Content.ReadFromJsonAsync<TResponse>()
                ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, request);
            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentLength == 0)
            {
                if (typeof(TResponse).IsValueType)
                    return default!;

                return default!;
            }

            if (typeof(TResponse) == typeof(string) || typeof(TResponse) == typeof(int) || typeof(TResponse) == typeof(decimal))
            {
                var plainText = await response.Content.ReadAsStringAsync();
                return (TResponse)Convert.ChangeType(plainText, typeof(TResponse));
            }

            return await response.Content.ReadFromJsonAsync<TResponse>()
                ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<TResponse> PostAsync<TResponse>(string endpoint)
    {
        try
        {
            var response = await _httpClient.PostAsync(endpoint, null);
            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentLength == 0)
            {
                if (typeof(TResponse).IsValueType)
                    return default!;

                return default!;
            }

            if (typeof(TResponse) == typeof(string) || typeof(TResponse) == typeof(int) || typeof(TResponse) == typeof(decimal))
            {
                var plainText = await response.Content.ReadAsStringAsync();
                return (TResponse)Convert.ChangeType(plainText, typeof(TResponse));
            }

            return await response.Content.ReadFromJsonAsync<TResponse>()
                ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
        catch (Exception)
        {
            
            throw ;
        }
    }


    public async Task<bool> PostBoolAsync<TRequest>(string endpoint, TRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }
        catch (Exception)
        {
            // Re-throw exception to propagate errors to the caller
            throw;
        }
    }

    public async Task<int> PostIntAsync<TRequest>(string endpoint, TRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, request);
            response.EnsureSuccessStatusCode();



            var result = await response.Content.ReadFromJsonAsync<int>();
            return result;
        }
        catch (Exception)
        {
            // Re-throw exception to propagate errors to the caller
            throw;
        }
    }



    public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, request);
            response.EnsureSuccessStatusCode();

            // Check for empty response content
            if (response.Content.Headers.ContentLength == 0)
            {
                // Handle empty content by returning a default value for value types (e.g., int, decimal)
                if (typeof(TResponse).IsValueType)
                    return default!;

                // For reference types, return null
                return default!;
            }

            // Handle plain text or primitive type responses
            if (typeof(TResponse) == typeof(string) || typeof(TResponse) == typeof(int) || typeof(TResponse) == typeof(decimal))
            {
                var plainText = await response.Content.ReadAsStringAsync();

                // Convert the plain text to the expected type
                return (TResponse)Convert.ChangeType(plainText, typeof(TResponse));
            }

            return await response.Content.ReadFromJsonAsync<TResponse>()
                ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
        catch (Exception)
        {

            throw;
        }
    }
}
