using System.Text.Json.Serialization;

namespace DrinksInfoAPI.ConsoleApp.Models;

public class IngredientResponse
{
    [JsonPropertyName("ingredients")]
    public List<Ingredient>? Ingredient { get; set; }
}