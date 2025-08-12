using System.Text.Json.Serialization;

namespace DrinksInfoAPI.ConsoleApp.Models;

public class Cocktail
{
    [JsonPropertyName("idDrink")]
    public string? Id { get; set; }
    
    [JsonPropertyName("strDrink")]
    public string? Name { get; set; }
    
    [JsonPropertyName("strCategory")]
    public string? Category { get; set; }
    
    [JsonPropertyName("strAlcoholic")]
    public string? Alcoholic { get; set; }
    
    [JsonPropertyName("strGlass")]
    public string? Glass { get; set; }
}