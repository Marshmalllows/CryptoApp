using System.Windows;
using System.Windows.Input;

namespace CryptoApp.Commands;

public class MoveWindowCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? obj)
    {
        var window = obj as Window ?? Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
        window?.DragMove();
    }

    public event EventHandler? CanExecuteChanged;
}