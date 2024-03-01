using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock_Explorer.Migrations;
using Stock_Explorer.Models;

namespace Stock_Explorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public StocksController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public ActionResult<List<Stock>> GetAll()
        {
            return this.data.Stocks.Where(x => x.Name == "APPL").ToList();
        }
    }
}
