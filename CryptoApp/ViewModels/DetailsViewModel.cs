using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
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

    private async void RefreshItems()
    {
        if (string.IsNullOrWhiteSpace(_searchText))
        {
            FilteredItems.Clear();
            return;
        }
        
        var assets = await ApiService.GetSearchResults(SearchText);

        FilteredItems.Clear();
        
        if (assets.Data is null)
        {
            return;
        }
        
        for (var i = 0; i < assets.Data.Count && i < 10; i++)
        {
            FilteredItems.Add(assets.Data[i].Name! + $" ({assets.Data[i].Symbol})");
        }
    }
}