using System.Net.Http;
using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Services;

public static class ApiService
{
    private static readonly HttpClient HttpClient = new();

    static ApiService()
    {
        HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoApp/1.0");
    }

    public static async Task<List<CoinMarketData>> GetAssetsAsync()
    {
        const string url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1";
        var response = await HttpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<CoinMarketData>>(response) ?? [];
    }

    public static async Task<SearchResult> GetSearchResultsAsync(string searchText)
    {
        var url = $"https://api.coingecko.com/api/v3/search?query={Uri.EscapeDataString(searchText)}";
        var response = await HttpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<SearchResult>(response) ?? new();
    }

    public static async Task<CoinDetails?> GetCoinDetailsAsync(string coinId)
    {
        var url = $"https://api.coingecko.com/api/v3/coins/{coinId}?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false";
        var response = await HttpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<CoinDetails>(response);
    }
}
