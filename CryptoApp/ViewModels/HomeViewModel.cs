using System.Collections.ObjectModel;
using CryptoApp.Models;
using CryptoApp.Services;

namespace CryptoApp.ViewModels;

public class HomeViewModel : BaseViewModel
{
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
        try
        {
            var data = await ApiService.GetAssetsAsync();

            TopChartsTable.Clear();

            foreach (var t in data)
            {
                var rank = t.MarketCapRank ?? 0;
                var currencyName = t.Name + $" ({t.Symbol?.ToUpper()})";
                var price = (t.CurrentPrice ?? 0).ToString("G10");
                var changes = (t.PriceChangePercentage24h ?? 0).ToString("G10");
                var capitalisation = (t.MarketCap ?? 0).ToString("G10");
                TopChartsTable.Add(new TopChartsTable(rank, currencyName, price, changes, capitalisation));
            }
        }
        catch
        {
            // Network error — table stays empty
        }
    }
}
