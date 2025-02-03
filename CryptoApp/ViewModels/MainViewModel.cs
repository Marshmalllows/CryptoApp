using System.Windows.Input;
using CryptoApp.Commands;

namespace CryptoApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel _selectedViewModel = new HomeViewModel();

    public BaseViewModel SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
    
    public ICommand UpdateViewCommand { get; }
    
    public ICommand CloseWindowCommand { get; }
    
    public ICommand MinimizeWindowCommand { get; }
    
    public ICommand MoveWindowCommand { get; }

    public MainViewModel()
    {
        UpdateViewCommand = new UpdateViewCommand(this);
        CloseWindowCommand = new CloseWindowCommand();
        MinimizeWindowCommand = new MinimizeWindowCommand();
        MoveWindowCommand = new MoveWindowCommand();
    }
}