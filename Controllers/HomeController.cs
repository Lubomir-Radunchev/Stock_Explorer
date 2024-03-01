using Microsoft.AspNetCore.Mvc;
using Stock_Explorer.DTO;
using Stock_Explorer.Models;
using System.Composition;
using System.Diagnostics;

namespace Stock_Explorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var stock = new SearchCriteria();
            return View(stock);
        }

        [HttpPost]
        public IActionResult Index(SearchCriteria stock)
        {
            List<Stock> stocks = new List<Stock>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7037/api/");
                //HTTP GET
                var responseTask = client.GetAsync("stocks");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<List<Stock>>();
                    readTask.Wait();

                    stocks = readTask.Result;
                }
                else //web api sent error response 
                {
                    stocks = new List<Stock> { };

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            stocks = stocks.Where(x => x.Name == stock.Name).ToList();


            TempData["stocks"] = stocks;


            return RedirectToAction("ShowStocks");
        }


        public IActionResult ShowStocks()
        {
            List<Stock> stocks = (List<Stock>)TempData["stocks"];

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
