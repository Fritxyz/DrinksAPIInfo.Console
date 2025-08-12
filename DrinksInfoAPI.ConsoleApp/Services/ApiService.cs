using System.Text.Json;
using DrinksInfoAPI.ConsoleApp.Models;

namespace DrinksInfoAPI.ConsoleApp.Services;

internal class ApiService :  IApiService
{
    private readonly HttpClient _httpClient = new();

    public async Task<T> GetDataAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(json);
        
        return result;
    }
}