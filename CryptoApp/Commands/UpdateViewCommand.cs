using System.Windows.Input;
using CryptoApp.ViewModels;

namespace CryptoApp.Commands;

public class UpdateViewCommand(MainViewModel viewModel) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter!.ToString() == "Home")
        {
            viewModel.SelectedViewModel = new HomeViewModel();
        }
        else if (parameter.ToString() == "Details")
        {
            viewModel.SelectedViewModel = new DetailsViewModel();
        }
    }

    public event EventHandler? CanExecuteChanged;
}