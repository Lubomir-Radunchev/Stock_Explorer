using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Stock_Explorer.Migrations;
using Stock_Explorer.Models;
using System.Net;

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

        //public ActionResult<List<Stock>> GetAll()
        //{
        //    return this.data.Stocks.Where(x => x.Name == "APPL").ToList();
        //}

        //public ActionResult Get(int id)
        //{
        //    string BaseURL = "https://yfapi.net/v6/finance/quote?symbols=AAPL";

        //    string addSymbol = "%2C";
        //    string URL = BaseURL;

        //    foreach (string stock in stocks)
        //    {
        //        URL += addSymbol + stock;
        //    }

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        //    request.Headers.Add("X-API-KEY", "[My API key]");

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Console.WriteLine(response.ContentType);
        //    Console.WriteLine(response.StatusCode);
        //}
    }
}
