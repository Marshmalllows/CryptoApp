using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using CryptoApp.Commands;
using CryptoApp.Models;
using CryptoApp.Services;

namespace CryptoApp.ViewModels;

public class DetailsViewModel : BaseViewModel
{
    private string _searchText = "Search...";
    
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
    
    private string _linkText = "";
    
    public string LinkText
    {
        get => _linkText;
        set
        {
            _linkText = value;
            OnPropertyChanged(nameof(LinkText));
        }
    }

    public ICommand OpenLinkCommand { get; } = new OpenLinkCommand();

    private ObservableCollection<string> _filteredItems = [];
    
    public ObservableCollection<string> FilteredItems
    {
        get => _filteredItems;
        set
        {
            _filteredItems = value;
            OnPropertyChanged(nameof(FilteredItems));
        }
    }

    private DetailsText? _detailsText;
    
    public DetailsText? DetailsText
    {
        get => _detailsText;
        set
        {
            _detailsText = value;
            OnPropertyChanged(nameof(DetailsText));
        }
    }
    
    private async void RefreshItems()
    {
        if (string.IsNullOrWhiteSpace(_searchText))
        {
            FilteredItems.Clear();
            return;
        }
        
        var assets = await ApiService.GetSearchResultsAsync(SearchText);

        FilteredItems.Clear();
        
        if (assets.Data is null)
        {
            return;
        }
        
        for (var i = 0; i < assets.Data.Count && i < 10; i++)
        {
            FilteredItems.Add(assets.Data[i].Name! + $" ({assets.Data[i].Symbol})");
        }
        
        ShowDetails();
    }

    private async void ShowDetails()
    {
        var details = await ApiService.GetSearchResultsAsync(SearchText);

        if (details.Data is null || details.Data.Count == 0)
        {
            return;
        }
        
        var data = details.Data[0];

        LinkText = "Official site";
        var name = data.Name ?? "no data :(";
        var symbol = data.Symbol ?? "no data :(";
        var rank = data.Rank ?? "no data :(";
        var price = data.PriceUsd ?? "no data :(";
        var supply = data.Supply ?? "no data :(";
        var capitalisation = data.MarketCapUsd ?? "no data :(";
        var volume = data.VolumeUsd24Hr ?? "no data :(";
        var change = data.ChangePercent24Hr ?? "no data :(";
        var vwap = data.Vwap24Hr ?? "no data :(";
        var changeString = "Last 24h price change: ";

        symbol = $"Code symbol: {symbol}";
        rank = $"Rank on market: {rank}";
        price = data.PriceUsd is null
            ? $"Current price: {price}"
            : $"Current price: {decimal.Parse(price, CultureInfo.InvariantCulture):G10}$";
        supply = data.Supply is null
            ? $"Currency supply: {supply}"
            : $"Currency supply: {decimal.Parse(supply, CultureInfo.InvariantCulture):G10}$";
        capitalisation = data.MarketCapUsd is null
            ? $"Market capitalisation: {capitalisation}"
            : $"Market capitalisation: {decimal.Parse(capitalisation, CultureInfo.InvariantCulture):G10}$";
        volume = data.VolumeUsd24Hr is null
            ? $"Last 24h volume transferred: {volume}"
            : $"Last 24h volume transferred: {decimal.Parse(volume, CultureInfo.InvariantCulture):G10}$";
        vwap = data.Vwap24Hr is null
            ? $"Last 24h VWAP: {vwap}"
            : $"Last 24h VWAP: {decimal.Parse(vwap, CultureInfo.InvariantCulture):G10}$";
        

        if (data.ChangePercent24Hr is not null)
        {
            changeString = $"{decimal.Parse(data.ChangePercent24Hr, CultureInfo.InvariantCulture):G10}%";

            switch (decimal.Parse(data.ChangePercent24Hr, CultureInfo.InvariantCulture))
            {
                case > 0:
                    changeString += " 📈";
                    break;
                case < 0:
                    changeString += " 📉";
                    break;
            }
        }
        
        DetailsText = new DetailsText(name, symbol, rank, price, supply, capitalisation, volume, changeString, vwap,
            data.Explorer!);
    }
}