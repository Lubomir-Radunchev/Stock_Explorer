using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stock_Explorer.DTO;
using Stock_Explorer.Models;
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
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            // Set the base address
            _client.BaseAddress = new Uri("https://alpha.financeapi.net/symbol/get-chart");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var stock = new SearchCriteria();
            return View(stock);
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






            var request = new HttpRequestMessage(HttpMethod.Get, $"get-chart?period=3M&symbol=AAPL");

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


                var correct = stockInfo.attributes.OrderByDescending(x => x.Key);

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
            List<ChartData> stocks = JsonConvert.DeserializeObject<List<ChartData>>(TempData["stocks"] as string);

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
