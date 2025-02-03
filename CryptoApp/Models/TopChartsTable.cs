namespace CryptoApp.Models;

public class TopChartsTable(int top, string currency, string price, string change, string capitalisation)
{
    public int Top { get; set; } = top;

    public string Currency { get; set; } = currency;

    public string Price { get; set; } = price;

    public string Change { get; set; } = change;
    
    public string Capitalisation { get; set; } = capitalisation;

}