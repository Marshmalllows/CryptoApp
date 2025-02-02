using System.Text.Json.Serialization;

namespace CryptoApp.Models;

public class DetailsText(
    string currencyName,
    string codeSymbolText,
    string rankText,
    string priceText,
    string supplyText,
    string marketCapUsdText,
    string volumeUsd24HrText,
    string changePercent24HrText,
    string vwap24HrText,
    string explorerText)
{
    public string CurrencyName { get; set; } = currencyName;

    public string CodeSymbolText { get; set; } = codeSymbolText;

    public string RankText { get; set; } = rankText;

    public string PriceText { get; set; } = priceText;

    public string SupplyText { get; set; } = supplyText;
    
    public string MarketCapUsdText { get; set; } = marketCapUsdText;

    public string VolumeUsd24HrText { get; set; } = volumeUsd24HrText;

    public string ChangePercent24HrText { get; set; } = changePercent24HrText;

    public string Vwap24HrText { get; set; } = vwap24HrText;

    public string ExplorerText { get; set; } = explorerText;
}