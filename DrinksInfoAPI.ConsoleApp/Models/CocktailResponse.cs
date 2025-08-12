using System.Text.Json.Serialization;

namespace DrinksInfoAPI.ConsoleApp.Models;

public class CocktailResponse
{
    [JsonPropertyName("drinks")]
    public List<Cocktail>? Cocktails { get; set; }
}