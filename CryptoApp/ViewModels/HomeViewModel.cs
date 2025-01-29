using System.Collections.ObjectModel;
using CryptoApp.Models;
using CryptoApp.Services;

namespace CryptoApp.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private AssetsModel _assetsModel;

    private ObservableCollection<string> _topChartsStrings = [];

    public ObservableCollection<string> TopChartsStrings
    {
        get => _topChartsStrings;
        set
        {
            _topChartsStrings = value;
            OnPropertyChanged(nameof(TopChartsStrings));
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
        
        TopChartsStrings.Clear();
        
        for (var i = 0; i < data.Count; i++)
        {
            var coinInfo = $"{i + 1}. {data[i].Name} ({data[i].Symbol}) Price:{data[i].PriceUsd}$";
            TopChartsStrings.Add(coinInfo);
        }
    }
}