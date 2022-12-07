using StockData.Infrastructure.BusinessObject;

namespace StockData.Infrastructure.Services
{
    public interface IStockPriceService
    {
        void GetData();
        void SaveStockPrices(List<StockPrice> listOfStockPrice);
    }
}