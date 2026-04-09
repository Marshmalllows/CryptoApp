using System.Text.Json.Serialization;

namespace CryptoApp.Models;

public class CoinMarketData
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("symbol")] public string? Symbol { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("current_price")] public decimal? CurrentPrice { get; set; }
    [JsonPropertyName("market_cap")] public decimal? MarketCap { get; set; }
    [JsonPropertyName("market_cap_rank")] public int? MarketCapRank { get; set; }
    [JsonPropertyName("total_volume")] public decimal? TotalVolume { get; set; }
    [JsonPropertyName("price_change_percentage_24h")] public decimal? PriceChangePercentage24h { get; set; }
    [JsonPropertyName("circulating_supply")] public decimal? CirculatingSupply { get; set; }
    [JsonPropertyName("total_supply")] public decimal? TotalSupply { get; set; }
}

public class SearchCoin
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("symbol")] public string? Symbol { get; set; }
    [JsonPropertyName("market_cap_rank")] public int? MarketCapRank { get; set; }
}

public class SearchResult
{
    [JsonPropertyName("coins")] public List<SearchCoin>? Coins { get; set; }
}

public class CoinDetails
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("symbol")] public string? Symbol { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("market_cap_rank")] public int? MarketCapRank { get; set; }
    [JsonPropertyName("market_data")] public CoinMarketDetails? MarketData { get; set; }
    [JsonPropertyName("links")] public CoinLinks? Links { get; set; }
}

public class CoinMarketDetails
{
    [JsonPropertyName("current_price")] public Dictionary<string, decimal>? CurrentPrice { get; set; }
    [JsonPropertyName("market_cap")] public Dictionary<string, decimal>? MarketCap { get; set; }
    [JsonPropertyName("total_volume")] public Dictionary<string, decimal>? TotalVolume { get; set; }
    [JsonPropertyName("price_change_percentage_24h")] public decimal? PriceChangePercentage24h { get; set; }
    [JsonPropertyName("circulating_supply")] public decimal? CirculatingSupply { get; set; }
    [JsonPropertyName("total_supply")] public decimal? TotalSupply { get; set; }
}

public class CoinLinks
{
    [JsonPropertyName("homepage")] public List<string>? Homepage { get; set; }
    [JsonPropertyName("blockchain_site")] public List<string>? BlockchainSite { get; set; }
}
