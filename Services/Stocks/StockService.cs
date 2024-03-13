
using Stock_Explorer.Migrations;
using Stock_Explorer.Models;

namespace Stock_Explorer.Services.Stocks
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext data;

        public StockService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name should have a value");
            }

            if (this.data.Stocks.FirstOrDefault(x => x.Name == name) != null)
            {
                throw new Exception("you can not add multiple times ");
            }

            Stock stock = new Stock();

            stock.Name = name;
            stock.LastUpdate = DateTime.UtcNow;


            this.data.Stocks.Add(stock);
            this.data.SaveChanges();
        }
    }
}
