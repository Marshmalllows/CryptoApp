using System.Collections.ObjectModel;
using System.Windows.Input;
using CryptoApp.Commands;
using CryptoApp.Models;
using CryptoApp.Services;

namespace CryptoApp.ViewModels;

public class DetailsViewModel : BaseViewModel
{
    private string _searchText = "";
    private string _linkText = "";
    private ObservableCollection<string> _filteredItems = [];
    private DetailsText? _detailsText;
    private readonly Dictionary<string, string> _coinIds = new(); // display name -> coin id

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
            RefreshItems();
        }
    }

    public string LinkText
    {
        get => _linkText;
        set
        {
            _linkText = value;
            OnPropertyChanged(nameof(LinkText));
        }
    }

    public ObservableCollection<string> FilteredItems
    {
        get => _filteredItems;
        set
        {
            _filteredItems = value;
            OnPropertyChanged(nameof(FilteredItems));
        }
    }

    public DetailsText? DetailsText
    {
        get => _detailsText;
        set
        {
            _detailsText = value;
            OnPropertyChanged(nameof(DetailsText));
        }
    }

    public ICommand OpenLinkCommand { get; }
    public ICommand ItemSelectedCommand { get; }

    public DetailsViewModel()
    {
        OpenLinkCommand = new OpenLinkCommand();
        ItemSelectedCommand = new ItemSelectedCommand(this);
    }

    private async void RefreshItems()
    {
        if (string.IsNullOrWhiteSpace(_searchText))
        {
            FilteredItems.Clear();
            _coinIds.Clear();
            return;
        }

        try
        {
            var result = await ApiService.GetSearchResultsAsync(SearchText);

            FilteredItems.Clear();
            _coinIds.Clear();

            if (result.Coins is null) return;

            for (var i = 0; i < result.Coins.Count && i < 5; i++)
            {
                var coin = result.Coins[i];
                var displayName = $"{coin.Name} ({coin.Symbol?.ToUpper()})";
                FilteredItems.Add(displayName);
                if (coin.Id != null)
                    _coinIds[displayName] = coin.Id;
            }
        }
        catch
        {
            // Network error — leave list empty
        }
    }

    public async void ShowDetails()
    {
        try
        {
        if (!_coinIds.TryGetValue(SearchText, out var coinId))
        {
            var search = SearchText.Split('(').First().Trim();
            var fallback = await ApiService.GetSearchResultsAsync(search);
            coinId = fallback.Coins?.FirstOrDefault()?.Id;
            if (coinId is null) return;
        }

        var data = await ApiService.GetCoinDetailsAsync(coinId);
        if (data is null || data.MarketData is null) return;

        LinkText = "Official site";
        var name = data.Name ?? "no data :(";
        var symbol = $"Code symbol: {data.Symbol?.ToUpper() ?? "no data :("}";
        var rank = $"Rank on market: {data.MarketCapRank?.ToString() ?? "no data :("}";

        var priceVal = data.MarketData.CurrentPrice?.GetValueOrDefault("usd");
        var price = priceVal.HasValue
            ? $"Current price: {priceVal.Value:G10}$"
            : "Current price: no data :(";

        var supplyVal = data.MarketData.CirculatingSupply;
        var supply = supplyVal.HasValue
            ? $"Currency supply: {supplyVal.Value:G10}"
            : "Currency supply: no data :(";

        var capVal = data.MarketData.MarketCap?.GetValueOrDefault("usd");
        var capitalisation = capVal.HasValue
            ? $"Market capitalisation: {capVal.Value:G10}$"
            : "Market capitalisation: no data :(";

        var volVal = data.MarketData.TotalVolume?.GetValueOrDefault("usd");
        var volume = volVal.HasValue
            ? $"Last 24h volume transferred: {volVal.Value:G10}$"
            : "Last 24h volume transferred: no data :(";

        var changeVal = data.MarketData.PriceChangePercentage24h;
        var changeString = "Last 24h price change: ";
        if (changeVal.HasValue)
        {
            changeString += $"{changeVal.Value:G10}%";
            changeString += changeVal.Value switch
            {
                > 0 => " 📈",
                < 0 => " 📉",
                _ => ""
            };
        }
        else
        {
            changeString += "no data :(";
        }

        var homepageUrl = data.Links?.Homepage?.FirstOrDefault(h => !string.IsNullOrEmpty(h)) ?? "";

        DetailsText = new DetailsText(name, symbol, rank, price, supply, capitalisation,
            volume, changeString, "Last 24h VWAP: N/A", homepageUrl);
        }
        catch
        {
            // Network error — details remain unchanged
        }
    }
}
