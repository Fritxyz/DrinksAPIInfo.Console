using Spectre.Console;

namespace DrinksInfoAPI.ConsoleApp.Views;

internal static class TableView
{
    internal static void GenerateTable<T>(IEnumerable<T> response)
    {
        Table table = new Table();
        
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            table.AddColumn(property.Name);   
        }
        
        foreach (var item in response)
        {
            var row = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(item)?.ToString();
                row.Add(value!);
            }
            
            table.AddRow(row.ToArray());
        }
        
        AnsiConsole.Write(table);
    }
}