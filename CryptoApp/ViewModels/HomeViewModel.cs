using System.Collections.ObjectModel;
using System.Globalization;
using CryptoApp.Models;
using CryptoApp.Services;

namespace CryptoApp.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private AssetsModel _assetsModel = new();

    private ObservableCollection<TopChartsTable> _topChartsTable = [];

    public ObservableCollection<TopChartsTable> TopChartsTable
    {
        get => _topChartsTable;
        set
        {
            _topChartsTable = value;
            OnPropertyChanged(nameof(TopChartsTable));
        }
    }

    public HomeViewModel()
    {
        _ = LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        _assetsModel = await ApiService.GetAssetsAsync();
        var data = _assetsModel.Data;

        if (data is null)
        {
            return;
        }

        TopChartsTable.Clear();

        foreach (var t in data)
        {
            var rank = int.Parse(t.Rank!);
            var currencyName = t.Name + $" ({t.Symbol})";
            var price = decimal.Parse(t.PriceUsd!, CultureInfo.InvariantCulture).ToString("G10");
            var changes = decimal.Parse(t.ChangePercent24Hr!, CultureInfo.InvariantCulture).ToString("G10");
            var capitalisation = decimal.Parse(t.MarketCapUsd!, CultureInfo.InvariantCulture).ToString("G10");
            TopChartsTable.Add(new TopChartsTable(rank, currencyName, price, changes, capitalisation));
        }
    }
}