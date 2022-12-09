using StockData.Infrastructure.BusinessObject;

namespace StockData.Infrastructure.Services
{
    public interface IStockPriceService
    {
        void SaveStockPrices(List<StockPrice> listOfStockPrice);
    }
}