using Stock_Explorer.Migrations;
using Stock_Explorer.Models;
using System.Linq;

namespace Stock_Explorer.Services.Records
{
    public class StockRecordService : IStockRecordsService
    {
        private readonly ApplicationDbContext data;
        public StockRecordService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public void Add(List<StockRecord> records, Stock stock)
        {
            var lastRecord = this.data
                            .StockRecords
                            .Where(x => x.StockId == stock.Id)
                            .LastOrDefault();
            if (lastRecord == null)
            {
                this.data.AddRange(records);
            }
            else
            {            // check.
                var diffOfDates = DateTime.UtcNow - stock.LastUpdate;


                var newestRecord = records.Take(diffOfDates.Days);


                this.data.AddRange(newestRecord);
            }
        }

    }
}
