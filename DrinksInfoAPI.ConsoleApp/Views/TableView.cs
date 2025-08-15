using System.ComponentModel.DataAnnotations;
using Spectre.Console;

namespace DrinksInfoAPI.ConsoleApp.Views;

internal static class TableView
{
    internal static void GenerateTable<T>(IEnumerable<T> response)
    {
        Table table = new Table();
        var properties = typeof(T).GetProperties();
        
        var nonEmptyProperties = properties
            .Where(p => response.Any(item =>
                !string.IsNullOrEmpty(p.GetValue(item)?.ToString())))
            .ToArray();

        foreach (var property in nonEmptyProperties)
        {
            table.AddColumn(property.Name);   
        }
        
        foreach (var item in response)
        {
            var row = new List<string>();

            foreach (var property in nonEmptyProperties)
            {
                var value = property.GetValue(item)?.ToString();
                row.Add(value!);
            }
            
            table.AddRow(row.ToArray());
        }
        
        AnsiConsole.Write(table);
    }
}