namespace CryptoApp.Models;

public class TopChartsTable(int top, string currency, decimal price, decimal change)
{
    public int Top { get; set; } = top;

    public string Currency { get; set; } = currency;

    public decimal Price { get; set; } = price;

    public decimal Change { get; set; } = change;
}