using Autofac;
using HtmlAgilityPack;
using StockData.Infrastructure.BusinessObject;
using StockData.Infrastructure.Services;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IStockPriceService _stockPriceService;


        public Worker(ILogger<Worker> logger, IStockPriceService stockPriceService)
        {
            _logger = logger;
            _stockPriceService = stockPriceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var url = "https://www.dse.com.bd/latest_share_price_scroll_l.php";

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);

                var status = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/header[1]/div[1]/span[3]/span[1]/b[1]").InnerHtml;

                if (status.ToLower() == "closed")
                {
                    var tableData = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/section[1]/div[1]/div[3]/div[1]/div[2]/div[1]/table[1]");

                    var listOfStockPrice = new List<StockPrice>();
                    StockPrice stockPrice = new StockPrice();

                    foreach (var nNode in tableData.Descendants("td"))
                    {
                        if (nNode.NodeType == HtmlNodeType.Element)
                        {
                            var xpath = nNode.XPath;

                            String[] spearator = { "[", "]" };
                            var indexOfElement = xpath.Split(spearator, StringSplitOptions.RemoveEmptyEntries).Last();

                            if (indexOfElement == "1")
                            {
                                stockPrice = new StockPrice();
                            }

                            var innterText = nNode.InnerText;
                            innterText = innterText.Replace(",", "");

                            if (indexOfElement == "2")
                            {
                                var companyName = innterText.Replace("\t", "").Replace("\r", "").Replace("\n", "");

                                stockPrice.CompanyName = companyName;
                            }
                            else if (innterText == "--")
                            {
                                CreateObject(stockPrice, indexOfElement, null);
                            }
                            else
                            {
                                double? parseInnerText = null;
                                try
                                {
                                    parseInnerText = double.Parse(innterText);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex.Message);
                                }

                                CreateObject(stockPrice, indexOfElement, parseInnerText);
                            }

                            if (indexOfElement == "11")
                            {
                                listOfStockPrice.Add(stockPrice);
                            }
                        }
                    }

                    _stockPriceService.SaveStockPrices(listOfStockPrice);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }

        private void CreateObject(StockPrice stockPrice, string indexOfElement, double? innterText)
        {
            if (indexOfElement == "1")
            {
                // Serial Number
            }
            else if (indexOfElement == "3")
            {
                stockPrice.LastTradingPrice = innterText;
            }
            else if (indexOfElement == "4")
            {
                stockPrice.High = innterText;
            }
            else if (indexOfElement == "5")
            {
                stockPrice.Low = innterText;
            }
            else if (indexOfElement == "6")
            {
                stockPrice.ClosePrice = innterText;
            }
            else if (indexOfElement == "7")
            {
                stockPrice.YesterdayClosePrice = innterText;
            }
            else if (indexOfElement == "8")
            {
                stockPrice.Change = innterText;
            }
            else if (indexOfElement == "9")
            {
                stockPrice.Trade = innterText;
            }
            else if (indexOfElement == "10")
            {
                stockPrice.Value = innterText;
            }
            else if (indexOfElement == "11")
            {
                stockPrice.Volume = innterText;
            }
        }
    }
}