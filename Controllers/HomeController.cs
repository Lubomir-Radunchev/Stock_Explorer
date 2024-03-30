using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stock_Explorer.DTO;
using Stock_Explorer.Models;
using Stock_Explorer.Services.Records;
using Stock_Explorer.Services.Stocks;
using System.Data;
using System.Diagnostics;

namespace Stock_Explorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly IStockService stockService;
        private readonly IStockRecordsService recordService;

        public HomeController(IHttpClientFactory httpClientFactory, IStockRecordsService recordService, IStockService stockService)
        {
            _client = httpClientFactory.CreateClient();
            // Set the base address
            _client.BaseAddress = new Uri("https://alpha.financeapi.net/symbol/get-chart");
            this.stockService = stockService;
            this.recordService = recordService;
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
                var responseContent = await response.Content.ReadAsStringAsync();

                // CHECK IF THE STOCK IS FOUND IN THE DATABASE
                var lastStockInfo = this.stockService.GetStockByName(stock.Name);

                if (lastStockInfo is null)
                {
                    // CREATING THE TABLE
                    this.stockService.Add(stock.Name);
                }

                ChartData? stockInfo = JsonConvert.DeserializeObject<ChartData>(responseContent);

                lastStockInfo = this.stockService.GetStockByName(stock.Name);
                // poslednata akciq wkarana datata i 
                var diffOfDates = DateTime.UtcNow - lastStockInfo.LastUpdate;
                if (diffOfDates.Days > 1)
                {

                    // var newest record = recordsService.ADD(INT DAYS, stocksinfo);
                    var newestRecord = stockInfo.attributes.Reverse().Take(diffOfDates.Days).Where(x => DateTime.Parse(x.Key) > lastStockInfo.LastUpdate).ToList();

                    foreach (var item in newestRecord)
                    {
                        var record = new StockRecord()
                        {
                            Adj = item.Value.Adj,
                            Close = item.Value.Close,
                            High = item.Value.High,
                            Low = item.Value.Low,
                            Volume = item.Value.Volume,
                            Open = item.Value.Open,
                            Data = item.Key,
                            StockId = lastStockInfo.Id,

                        };

                        this.recordService.Add(record);
                    }
                    // MAKE THE FETCHING DATA ONLY FOR THE 
                    TempData["alert"] = "Newest information was added!";
                    return RedirectToAction("Index");
                }
                else if (diffOfDates.Days < 0)
                {

                    var newestRecord = stockInfo.attributes;

                    foreach (var item in stockInfo.attributes)
                    {
                        var record = new StockRecord()
                        {
                            Adj = item.Value.Adj,
                            Close = item.Value.Close,
                            High = item.Value.High,
                            Low = item.Value.Low,
                            Volume = item.Value.Volume,
                            Open = item.Value.Open,
                            Data = item.Key,
                            StockId = lastStockInfo.Id,

                        };

                        this.recordService.Add(record);
                    }
                }
                else if (diffOfDates.Days == 0)
                {
                    TempData["alert"] = "Stock is already updated!";
                    return RedirectToAction("Index");
                }

                TempData["alert"] = "Successful update!";
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






            var request = new HttpRequestMessage(HttpMethod.Get, $"get-chart?period=1Y&symbol={stock.Name}");

            // Add the required header
            request.Headers.Add("X-API-KEY", "3db8airwaIaFxjOS261v69J7U5civ2D85RW9JrUo");

            // Send the request and get the response
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                //List<ChartDataPoint> stockDataPoints = stockInfo?.Attributes?.DataPoints?.Values.ToList();
                ChartData? stockInfo = JsonConvert.DeserializeObject<ChartData>(responseContent);

                var lastStockInfo = this.stockService.GetStockByName(stock.Name);

                if (lastStockInfo is not null)
                {
                    // TODO: THROW ERROR BECAUSE ITS ALREDY IN THE DATABASE!!!!!!
                }
                else
                {
                    this.stockService.Add(stock.Name);
                    lastStockInfo = stockService.GetStockByName(stock.Name);
                }

                foreach (var item in stockInfo.attributes)
                {
                    var record = new StockRecord()
                    {
                        Adj = item.Value.Adj,
                        Close = item.Value.Close,
                        High = item.Value.High,
                        Low = item.Value.Low,
                        Volume = item.Value.Volume,
                        Open = item.Value.Open,
                        Data = item.Key,
                        StockId = lastStockInfo.Id,

                    };

                    this.recordService.Add(record);
                }

                var correct = stockInfo.attributes.OrderBy(x => x.Key).LastOrDefault();
                TempData["stocks"] = JsonConvert.SerializeObject(correct);

                return RedirectToAction("ShowStocks");
            }
            else
            {
                // Handle the error, e.g., log or return an error view
                return View("Error");
            }


        }


        public IActionResult ShowStocks(SearchCriteria searchCriteria = null)
        {
            //string jsonString = TempData["stocks"] as string;

            //Deserialize into a Dictionary<string, ChartDataPoint>

            // взимаме акцията 
            if (searchCriteria.Name == null)
            {

            }

            // взимаме рекордите на тази акция 


            // визуализираме я


            ChartDataTest stocks = JsonConvert.DeserializeObject<ChartDataTest>(searchCriteria.Name);

            //Convert string keys to DateTimeKey
            //Dictionary<string, ChartDataPoint> stocksDateTimeKeys = jsonString.ToDictionary(
            //    kvp => kvp.Key,  // Convert string key to DateTimeKey
            //    kvp => kvp.Value
            //);
            //return View(stocks);

            return View();
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
