using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stock_Explorer.DTO;
using Stock_Explorer.Migrations;
using Stock_Explorer.Models;
using Stock_Explorer.Services.Stocks;
using System;
using System.Composition;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Xml;

namespace Stock_Explorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly IStockService stockService;


        public HomeController(IHttpClientFactory httpClientFactory, IStockService stockService)
        {
            _client = httpClientFactory.CreateClient();
            // Set the base address
            _client.BaseAddress = new Uri("https://alpha.financeapi.net/symbol/get-chart");
            this.stockService = stockService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stock = new SearchCriteria();
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Check(SearchCriteria stock)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"get-chart?period=1Y&symbol={stock.Name}");

            // Add the required header
            request.Headers.Add("X-API-KEY", "3db8airwaIaFxjOS261v69J7U5civ2D85RW9JrUo");

            // Send the request and get the response
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //var responseContent = await response.Content.ReadAsStringAsync();

                //List<ChartData?> stockInfo = JsonConvert.DeserializeObject<List<ChartData>>(responseContent);

                this.stockService.Add(stock.Name);

                return RedirectToAction("Index");
            }
            else
            {
                // Handle the error, e.g., log or return an error view
                return View("Error");
            }


        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(SearchCriteria stock)
        {
            //List<Stock> stocks = new List<Stock>();
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://yfapi.net/v6/finance/quote?symbols=AAPL");
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //    request.Headers.Add("X-API-KEY", "[My API key]");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadFromJsonAsync<List<Stock>>();
            //        readTask.Wait();

            //        stocks = readTask.Result;
            //    }
            //    else //web api sent error response 
            //    {
            //        stocks = new List<Stock> { };

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}

            //stocks = stocks.Where(x => x.Name == stock.Name).OrderByDescending(x=> x.Date).ToList();


            //// TempData["stocks"] = JsonConvert.SerializeObject(stocks);
            //TempData["stocks"] = JsonConvert.SerializeObject(stocks);

            //return RedirectToAction("ShowStocks");






            var request = new HttpRequestMessage(HttpMethod.Get, $"get-chart?period=6M&symbol=AAPL");

            // Add the required header
            request.Headers.Add("X-API-KEY", "3db8airwaIaFxjOS261v69J7U5civ2D85RW9JrUo");

            // Send the request and get the response
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content




                var responseContent = await response.Content.ReadAsStringAsync();

                ChartData? stockInfo = JsonConvert.DeserializeObject<ChartData>(responseContent);

                ;
                //List<ChartDataPoint> stockDataPoints = stockInfo?.Attributes?.DataPoints?.Values.ToList();

                var correct = stockInfo.attributes.OrderBy(x => x.Key).LastOrDefault();
                TempData["stocks"] = JsonConvert.SerializeObject(correct);



                //var stockInfo = JsonConvert.DeserializeObject<StockInfo>(responseContent);

                //List<StockYahooDTO> stockDetails = stockInfo?.QuoteResponse?.Result;

                // Process 'result' as needed

                return RedirectToAction("ShowStocks");
            }
            else
            {
                // Handle the error, e.g., log or return an error view
                return View("Error");
            }


        }


        public IActionResult ShowStocks()
        {
            string jsonString = TempData["stocks"] as string;

            // Deserialize into a Dictionary<string, ChartDataPoint>
            ChartDataTest stocks = JsonConvert.DeserializeObject<ChartDataTest>(jsonString);

            // Convert string keys to DateTimeKey
            //Dictionary<string, ChartDataPoint> stocksDateTimeKeys = stocksStringKeys.ToDictionary(
            //    kvp => kvp.Key,  // Convert string key to DateTimeKey
            //    kvp => kvp.Value
            //);
            return View(stocks);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
