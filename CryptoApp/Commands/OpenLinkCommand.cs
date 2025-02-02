using System.Diagnostics;
using System.Windows.Input;

namespace CryptoApp.Commands;

public class OpenLinkCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return parameter is string url && !string.IsNullOrWhiteSpace(url);
    }

    public void Execute(object? parameter)
    {
        if (parameter is string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }

    public event EventHandler? CanExecuteChanged;
}