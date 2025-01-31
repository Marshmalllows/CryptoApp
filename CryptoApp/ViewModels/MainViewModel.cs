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

    public MainViewModel()
    {
        UpdateViewCommand = new UpdateViewCommand(this);
        CloseWindowCommand = new CloseWindowCommand();
        MinimizeWindowCommand = new MinimizeWindowCommand();
    }

    public ICommand UpdateViewCommand { get; set; }
    
    public ICommand CloseWindowCommand { get; set; }
    
    public ICommand MinimizeWindowCommand { get; set; }
}