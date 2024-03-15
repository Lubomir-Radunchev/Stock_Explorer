using Stock_Explorer.Models;

namespace Stock_Explorer.Services.Stocks
{
    public interface IStockService
    {
        void Add(string name);
        Stock? GetStockByName(string name);
    }
}
