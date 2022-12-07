using StockDataBO = StockData.Infrastructure.BusinessObject.StockPrice;
using StockDataEO = StockData.Infrastructure.Entities.StockPrice;

using CompanyEO = StockData.Infrastructure.Entities.Company;
using StockData.Infrastructure.UnitOfWorks;

namespace StockData.Infrastructure.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public StockPriceService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void GetData()
        {
            var x = _applicationUnitOfWork.Company.GetAll();
        }

        public void SaveStockPrices(List<StockDataBO> listOfStockPrice)
        {
            var companies = _applicationUnitOfWork.Company.GetAll();

            // companies.Where(x => x.)

            int i = 0;

            foreach (var x in listOfStockPrice)
            {
                var stockPrice = new StockDataEO();

                var companyId = companies.Where(y => y.TradeCode == x.CompanyName).Select(z => z.Id).FirstOrDefault();

                if (companyId == Guid.Empty || companyId == null)
                {
                    var companyObj = new CompanyEO();
                    companyObj.TradeCode = x.CompanyName;
                    stockPrice.Company = companyObj;
                }
                else
                {
                    stockPrice.CompanyId = companyId;
                }

                stockPrice.LastTradingPrice = x.LastTradingPrice;
                stockPrice.High = x.High;
                stockPrice.Low = x.Low;
                stockPrice.ClosePrice = x.ClosePrice;
                stockPrice.YesterdayClosePrice = x.YesterdayClosePrice;
                stockPrice.Trade = x.Trade;
                stockPrice.Change = x.Change;
                stockPrice.Value = x.Value;
                stockPrice.Volume = x.Volume;

                _applicationUnitOfWork.StockPrice.Add(stockPrice);

                i = i + 1;
                if (i == 5)
                {
                    break;
                }
            }

            _applicationUnitOfWork.Save();
        }
    }
}