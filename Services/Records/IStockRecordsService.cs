using Stock_Explorer.Models;

namespace Stock_Explorer.Services.Records
{
    public interface IStockRecordsService
    {
        public void Add(List<StockRecord> records,Stock stock);
        public void Add(StockRecord income);
    }
}
