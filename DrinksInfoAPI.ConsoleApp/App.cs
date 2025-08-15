using DrinksInfoAPI.ConsoleApp.UI;

namespace DrinksInfoAPI.ConsoleApp;

internal class App
{
    private readonly MenuUi _ui = new();

    internal void Run()
    {
        while (true)
        {
            int choiceNumber = MenuUi.ShowMenu();
            _ui.UserChoice(choiceNumber);
        }
    }
}