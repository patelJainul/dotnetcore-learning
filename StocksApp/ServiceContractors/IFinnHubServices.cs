using Models;

namespace ServiceContractors;

public interface IFinnHubServices
{
  Task<StockPriceQuote?> GetStockPriceQuote(string symbol);
  Task<CompanyProfile?> GetCompanyProfile(string stockSymbol);
}
