using System.Windows.Input;
using CryptoApp.ViewModels;

namespace CryptoApp.Commands;

public class ItemSelectedCommand(DetailsViewModel viewModel) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not string selectedItem) return;
        viewModel.SearchText = selectedItem;
        viewModel.ShowDetails();
    }

    public event EventHandler? CanExecuteChanged;
}