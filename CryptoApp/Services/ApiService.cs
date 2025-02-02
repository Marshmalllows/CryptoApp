using System.Net.Http;
using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Services;

public static class ApiService
{
    private static readonly HttpClient HttpClient = new();

    public static async Task<AssetsModel> GetAssetsAsync()
    {
        const string url = "https://api.coincap.io/v2/assets?limit=10";
        var response = await HttpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<AssetsModel>(response)!;
    }

    public static async Task<AssetsModel> GetSearchResultsAsync(string searchText)
    {
        var url = $"https://api.coincap.io/v2/assets?search={searchText}";
        var response = await HttpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<AssetsModel>(response)!;
    }
    
    public static async Task<AssetsModel> GetDetailsAsync(string currency)
    {
        var url = $"https://api.coincap.io/v2/assets?id={currency}";
        var response = await HttpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<AssetsModel>(response)!;
    }
}