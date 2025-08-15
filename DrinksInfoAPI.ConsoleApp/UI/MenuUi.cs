using DrinksInfoAPI.ConsoleApp.Controllers;
using Spectre.Console;

namespace DrinksInfoAPI.ConsoleApp.UI;

internal class MenuUi()
{
    private readonly CocktailController _cocktailController = new CocktailController();
    private readonly IngredientController _ingredientController = new IngredientController();

    #region Show_Menu_Method()
    internal static int ShowMenu()
    {
        AnsiConsole .MarkupLine("[bold yellow]🍹 Welcome to the Drinks Menu App![/]");
        
        Dictionary<int, string> choices = new Dictionary<int, string>()
        {
            { 1, "Search Cocktail by Name" },
            { 2, "List All Cocktails By First Letter" },
            { 3, "Search Ingredient By Name" },
            { 4, "Lookup Full Cocktail Details By Id" },
            { 5, "Lookup ingredient by ID" },
            { 6, "Lookup a Random Cocktail" },
            { 7, "Search by Ingredient" },
            { 0, "Application Exit"}
        };
        
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a [green]drink category[/]:")
                .AddChoices(choices.Values.ToArray()
                ));

        return choices.FirstOrDefault(x => x.Value == choice).Key;
    }
    #endregion

    #region User_Choice_Method
    internal void UserChoice(int choiceNumber)
    {
        switch (choiceNumber)
        {
            case 0:
                ExitApplication();
                break;
            case 1:
                _cocktailController.SearchCocktailByName();
                break;
            case 2:
                _cocktailController.ListAllCocktailsByFirstLetter();
                break;
            case 3:
                _ingredientController.SearchIngredientByName();
                break;
            case 4:
                _cocktailController.LookupFullCocktailsDetailsById();
                break;
            case 5:
                _ingredientController.LookupIngredientById();
                break;
            case 6:
                _cocktailController.LookupARandomCocktail();
                break;
            case 7:
                _cocktailController.SearchByIngredient();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Exit_Application_Method
    private static void ExitApplication()
    {
        Console.Clear();
        AnsiConsole .MarkupLine("[bold yellow]🍹Thank you for using the application. Press any key to exit[/]");
        Console.ReadKey();
        Environment.Exit(0);
    }
    #endregion
}