using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceContractors;

namespace StockAssignment.Controllers
{
  public class HomeController(IFinnHubServices finnHubServices) : Controller
  {
    [Route("/")]
    public async Task<ActionResult> Index()
    {
      string symbol = "AAPL";
      Stock? stock = await finnHubServices.GetStockQuote(symbol);
      return stock == null ? NotFound() : View(stock);
    }
  }
}
