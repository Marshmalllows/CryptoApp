using System.Windows.Input;
using CryptoApp.Services;
using CryptoApp.ViewModels;

namespace CryptoApp.Commands;

public class UpdateThemeCommand(MainViewModel viewModel) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (viewModel.SelectedTheme == "DarkTheme")
        {
            viewModel.SelectedTheme = "LightTheme";
            ThemeManager.ChangeTheme("LightTheme");
        }
        else if (viewModel.SelectedTheme == "LightTheme")
        {
            viewModel.SelectedTheme = "DarkTheme";
            ThemeManager.ChangeTheme("DarkTheme");
        }
    }

    public event EventHandler? CanExecuteChanged;
}