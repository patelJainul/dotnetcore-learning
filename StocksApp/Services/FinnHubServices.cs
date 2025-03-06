using System;
using System.Text.Json;
using Models;
using ServiceContractors;

namespace Services;

public class FinnHubServices(IHttpClientFactory httpClientFactory) : IFinnHubServices
{
  private readonly HttpClient _client = httpClientFactory.CreateClient("finnhub");

  public async Task<StockPriceQuote?> GetStockPriceQuote(string symbol)
  {
    string response = await _client.GetStringAsync($"quote?symbol={symbol}");

    var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
    if (data == null)
    {
      return null;
    }
    StockPriceQuote stock = new()
    {
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

  public async Task<CompanyProfile?> GetCompanyProfile(string stockSymbol)
  {
    string response = await _client.GetStringAsync($"stock/profile2?symbol={stockSymbol}");
    var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

    if (data == null)
    {
      return null;
    }
    CompanyProfile companyProfile = new()
    {
      Country = data["country"].ToString() ?? string.Empty,
      Currency = data["currency"].ToString() ?? string.Empty,
      Exchange = data["exchange"].ToString() ?? string.Empty,
      Industry = data["finnhubIndustry"].ToString() ?? string.Empty,
      Ipo = data["ipo"].ToString() ?? string.Empty,
      Logo = data["logo"].ToString() ?? string.Empty,
      MarketCapitalization = data["marketCapitalization"].ToString() ?? string.Empty,
      Name = data["name"].ToString() ?? string.Empty,
      Phone = data["phone"].ToString() ?? string.Empty,
      ShareOutstanding = data["shareOutstanding"].ToString() ?? string.Empty,
      Ticker = data["ticker"].ToString() ?? string.Empty,
      WebUrl = data["weburl"].ToString() ?? string.Empty,
    };
    return companyProfile;
  }
}
