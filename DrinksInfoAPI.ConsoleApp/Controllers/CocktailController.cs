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
        FetchAndDisplay(url, $"No drinks found with keyword: {cocktailName}");
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
        FetchAndDisplay(url, $"No drinks found with keyword: {letter}");
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
        FetchAndDisplay(url, $"No drinks found with keyword: {cocktailId}");
    }
    #endregion

    #region Lookup_Random_Cocktail_Method
    internal void LookupARandomCocktail()
    {
        string url = "https://www.thecocktaildb.com/api/json/v1/1/random.php";
        FetchAndDisplay(url, $"No drinks found");
    }
    #endregion

    #region Search_By_Ingredient_Method
    internal void SearchByIngredient()
    {
        string ingredient = AnsiConsole.Ask<string>("\nPlease enter the ingredient or press 0 to cancel the search:");
        if (ingredient == "0")
        {
            Console.Clear();
            return;
        }

        string url = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?i={ingredient}";
        FetchAndDisplay(url, $"No drinks found with keyword: {ingredient}");
    }
    #endregion

    #region Fetch_And_Display_Method
    private void FetchAndDisplay(string url, string noResultsMessage)
    {
        try
        {
            var response = _apiService.GetDataAsync<CocktailResponse>(url).Result;
            var cocktails = response?.Cocktails;
            
            if(cocktails == null || cocktails.Count == 0)
                AnsiConsole.MarkupLine($"[red]{noResultsMessage}[/]");
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