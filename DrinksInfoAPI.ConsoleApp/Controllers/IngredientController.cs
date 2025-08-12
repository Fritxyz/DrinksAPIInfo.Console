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
        
        try
        {
            var response = _apiService.GetDataAsync<IngredientResponse>(url);
            var ingredient = response?.Result.Ingredient;

            if (ingredient == null || ingredient.Count == 0)
                AnsiConsole.MarkupLine($"[red]No drinks found with keyword: {ingredientName}[/]");
            else
                TableView.GenerateTable(ingredient);
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
        
        AnsiConsole.Write("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
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
        
        try
        {
            var response = _apiService.GetDataAsync<IngredientResponse>(url);
            var ingredient = response?.Result.Ingredient;

            if (ingredient == null || ingredient.Count == 0)
                AnsiConsole.MarkupLine($"[red]No drinks found with keyword: {ingredientId}[/]");
            else
                TableView.GenerateTable(ingredient);
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
        
        AnsiConsole.Write("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }
    #endregion Search_Ingredient_By_Id_Method
    
}