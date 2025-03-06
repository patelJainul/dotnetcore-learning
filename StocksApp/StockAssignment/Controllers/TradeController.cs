using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.Configurations;
using ServiceContractors;
using ViewModels;

namespace StockAssignment.Controllers
{
  public class TradeController(
    IFinnHubServices finnHubServices,
    IOptions<TradingOptions> tradingOptions,
    IOptions<ApiSettings> apiOptions
  ) : Controller
  {
    [Route("/")]
    public async Task<ActionResult> Index()
    {
      string symbol = tradingOptions.Value.DefaultStockSymbol;
      StockPriceQuote? stock = await finnHubServices.GetStockPriceQuote(symbol);
      CompanyProfile? companyProfile = await finnHubServices.GetCompanyProfile(symbol);
      if (stock == null || companyProfile == null)
      {
        return NotFound();
      }
      StockTrade stockTrade = new()
      {
        Price = stock.CurrentPrice,
        Quantity = 1,
        StockSymbol = symbol,
        StockName = companyProfile.Name,
      };

      ViewBag.Token = apiOptions.Value.ApiKey;
      return View(stockTrade);
    }
  }
}
