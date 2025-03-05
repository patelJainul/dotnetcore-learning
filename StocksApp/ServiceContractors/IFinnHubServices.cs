using Models;

namespace ServiceContractors;

public interface IFinnHubServices
{
  public Task<Stock?> GetStockQuote(string symbol);
}
