using System.Windows.Input;
using CryptoApp.Commands;

namespace CryptoApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel _selectedViewModel = new HomeViewModel();
    
    private string _selectedTheme = "LightTheme";

    public BaseViewModel SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
    
    public string SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            _selectedTheme = value;
            OnPropertyChanged(nameof(SelectedTheme));
        }
    }
    
    public ICommand UpdateViewCommand { get; }
    
    public ICommand CloseWindowCommand { get; }
    
    public ICommand MinimizeWindowCommand { get; }
    
    public ICommand MoveWindowCommand { get; }
    
    public ICommand UpdateThemeCommand { get; }


    public MainViewModel()
    {
        UpdateViewCommand = new UpdateViewCommand(this);
        CloseWindowCommand = new CloseWindowCommand();
        MinimizeWindowCommand = new MinimizeWindowCommand();
        MoveWindowCommand = new MoveWindowCommand();
        UpdateThemeCommand = new UpdateThemeCommand(this);
    }
}