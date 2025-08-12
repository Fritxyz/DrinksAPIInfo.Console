namespace DrinksInfoAPI.ConsoleApp.Services;

public interface IApiService
{
    Task<T> GetDataAsync<T>(string url);
}