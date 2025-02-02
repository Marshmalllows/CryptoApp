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
        
        for (var i = 0; i < data.Count; i++)
        {
            var rank = int.Parse(data[i].Rank!);
            var currencyName = data[i].Name + $" ({data[i].Symbol})";
            var price = decimal.Parse(data[i].PriceUsd!, CultureInfo.InvariantCulture);
            var changes = decimal.Parse(data[i].ChangePercent24Hr!, CultureInfo.InvariantCulture);
            TopChartsTable.Add(new TopChartsTable(rank, currencyName, price, changes));
        }
    }
}