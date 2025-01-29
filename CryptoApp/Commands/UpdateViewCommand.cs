using System.Windows.Input;
using CryptoApp.ViewModels;

namespace CryptoApp.Commands;

public class UpdateViewCommand(MainViewModel viewModel) : ICommand
{
    private MainViewModel _viewModel = viewModel;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter!.ToString() == "Home")
        {
            _viewModel.SelectedViewModel = new HomeViewModel();
        }
        else if (parameter.ToString() == "Details")
        {
            _viewModel.SelectedViewModel = new DetailsViewModel();
        }
    }

    public event EventHandler? CanExecuteChanged;
}