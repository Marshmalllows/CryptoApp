using System.Windows;
using CryptoApp.ViewModels;

namespace CryptoApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}