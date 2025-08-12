using System.Text.Json;
using DrinksInfoAPI.ConsoleApp.Models;
using DrinksInfoAPI.ConsoleApp.Services;
using DrinksInfoAPI.ConsoleApp.Views;
using Spectre.Console;

namespace DrinksInfoAPI.ConsoleApp.Controllers;

internal class CocktailController()
{
    private readonly IApiService _apiService =  new ApiService();

    #region Search_Cocktail_By_Name_Method
    internal void SearchCocktailByName()
    {
        string cocktailName = AnsiConsole.Ask<string>("\nPlease enter the name or press 0 to cancel the search: ");
        if (cocktailName == "0")
        {
            Console.Clear();
            return;
        }

        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={cocktailName}";
        
        try
        {
            var response = _apiService.GetDataAsync<CocktailResponse>(url);
            var cocktails = response?.Result.Cocktails;

            if (cocktails == null || cocktails.Count == 0)
                AnsiConsole.MarkupLine($"[red]No drinks found with keyword: {cocktailName}[/]");
            else
                TableView.GenerateTable(cocktails);
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

    #region List_All_Cocktails_By_First_Letter_Method
    internal void ListAllCocktailsByFirstLetter()
    {
        char letter = AnsiConsole.Ask<char>("\nPlease enter the first letter or press 0 to cancel the search: "); 
        
        if (letter == '0')
        {
            Console.Clear();
            return;
        }

        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?f={letter}";
        
        try
        {
            var response = _apiService.GetDataAsync<CocktailResponse>(url);
            var cocktails = response?.Result.Cocktails;

            if (cocktails == null || cocktails.Count == 0)
                AnsiConsole.MarkupLine($"[red]No drinks found with letter: {letter}[/]");
            else
                TableView.GenerateTable(cocktails);
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
    
    #region Lookup_Full_Cocktail_Details_By_Id_Method
    internal void LookupFullCocktailsDetailsById()
    {
        int cocktailId = AnsiConsole.Ask<int>("\nPlease enter the ID or press 0 to cancel the search: ");
        if (cocktailId == 0)
        {
            Console.Clear();
            return;
        }

        string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={cocktailId}";
        
        try
        {
            var response = _apiService.GetDataAsync<CocktailResponse>(url);
            var cocktail = response?.Result.Cocktails;

            if (cocktail == null || cocktail.Count == 0)
                AnsiConsole.MarkupLine($"[red]No drinks found with keyword: {cocktailId}[/]");
            else
                TableView.GenerateTable(cocktail);
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
}