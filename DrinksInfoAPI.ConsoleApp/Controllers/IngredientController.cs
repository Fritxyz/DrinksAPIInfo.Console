using System.Text.Json;
using DrinksInfoAPI.ConsoleApp.Models;
using DrinksInfoAPI.ConsoleApp.Services;
using DrinksInfoAPI.ConsoleApp.Views;
using Spectre.Console;

namespace DrinksInfoAPI.ConsoleApp.Controllers;

public class IngredientController
{
    private readonly IApiService _apiService =  new ApiService();
    
    #region Search_Ingredient_By_Name_Method
    internal void SearchIngredientByName()
    {
        string ingredientName = AnsiConsole.Ask<string>("\nPlease enter the name or press 0 to cancel the search: ");
        if (ingredientName == "0")
        {
            Console.Clear();
            return;
        }

        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?i={ingredientName}";
        
        FetchAndDisplay(url, $"No ingredient found with keyword: {ingredientName}");
    }    
    #endregion
    
    #region Lookup_Ingredient_By_Id_Method
    internal void LookupIngredientById()
    {
        int ingredientId = AnsiConsole.Ask<int>("\nPlease enter the ID of the ingredient or press 0 to exit the lookup: ");
        if (ingredientId == 0)
        {
            Console.Clear();
            return;
        }

        string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?iid={ingredientId}";
        FetchAndDisplay(url, $"No ingredient found with keyword: {ingredientId}");
    }
    #endregion Search_Ingredient_By_Id_Method
    
    #region Fetch_And_Display_Method
    private void FetchAndDisplay(string url, string noResultsMessage)
    {
        try
        {
            var response = _apiService.GetDataAsync<IngredientResponse>(url).Result;
            var ingredients = response?.Ingredient;
            
            if(ingredients == null || ingredients.Count == 0)
                AnsiConsole.MarkupLine($"[red]{noResultsMessage}[/]");
            else
                TableView.GenerateTable(ingredients);
        }
        catch (HttpRequestException)
        {
            AnsiConsole.MarkupLine("[red]Failed to reach the API. Check your internet connection.[/]");
        }
        catch (JsonException)
        {
            AnsiConsole.MarkupLine("[red]Something went wrong while processing the data.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Unexpected error: {ex.Message}[/]");
        }

        PauseAndClear();
    }
    #endregion
    
    #region Pause_And_Clear_Method
    private void PauseAndClear()
    {
        AnsiConsole.Write("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }
    #endregion
}