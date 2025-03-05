namespace Models;

public class Stock
{
  public string StockSymbol { get; set; } = string.Empty;
  public float CurrentPrice { get; set; }
  public float Change { get; set; }
  public float PercentChange { get; set; }
  public float HighPriceOfDay { get; set; }
  public float LowPriceOfDay { get; set; }
  public float OpenPriceOfDay { get; set; }
  public float PreviousClosePrice { get; set; }
}
