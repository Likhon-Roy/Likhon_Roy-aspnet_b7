namespace StockData.Infrastructure.Entities
{
    public class StockPrice : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        public string? LastTradingPrice { get; set; }
        public string? High { get; set; }
        public string? Low { get; set; }
        public string? ClosePrice { get; set; }
        public string? YesterdayClosePrice { get; set; }
        public string? Change { get; set; }
        public string? Trade { get; set; }
        public string? Value { get; set; }
        public string? Volume { get; set; }
    }
}