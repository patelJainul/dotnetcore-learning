using System;
using System.Text.Json;
using Models;
using ServiceContractors;

namespace Services;

public class FinnHubServices : IFinnHubServices
{
  private readonly HttpClient _client;

  public FinnHubServices(IHttpClientFactory httpClientFactory)
  {
    _client = httpClientFactory.CreateClient("finnhub");
  }

  public async Task<Stock?> GetStockQuote(string symbol)
  {
    string response = await (
      await _client.GetAsync($"quote?symbol={symbol}")
    ).Content.ReadAsStringAsync();

    var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
    if (data == null)
    {
      return null;
    }
    Stock stock = new()
    {
      StockSymbol = symbol,
      CurrentPrice = float.Parse(data["c"].ToString() ?? "0"),
      Change = float.Parse(data["d"].ToString() ?? "0"),
      PercentChange = float.Parse(data["dp"].ToString() ?? "0"),
      HighPriceOfDay = float.Parse(data["h"].ToString() ?? "0"),
      LowPriceOfDay = float.Parse(data["l"].ToString() ?? "0"),
      OpenPriceOfDay = float.Parse(data["o"].ToString() ?? "0"),
      PreviousClosePrice = float.Parse(data["pc"].ToString() ?? "0"),
    };
    return stock;
  }
}
