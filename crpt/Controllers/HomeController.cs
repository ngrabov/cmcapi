using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using crpt.Models;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Threading.Tasks;
using crpt.Data;
using Microsoft.EntityFrameworkCore;

namespace crpt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CryptoContext _context; //needs decoupling; multilayering not yet implemented
        private static string API_KEY = "c7d83357-5d22-4a9c-898a-b85e3c2118be";

        public HomeController(ILogger<HomeController> logger, CryptoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var crl = JsonConvert.DeserializeObject<RootObject>(makeAPICall());

            foreach(var item in crl.data) //write to DB
            {
                var coin = await _context.Data.FirstOrDefaultAsync(c => c.name == item.name);
                if(coin != null) //already exists one with the same name
                {
                    coin.name = item.name;
                    coin.circulating_supply = item.circulating_supply;
                    coin.symbol = item.symbol;
                    coin.max_supply = item.max_supply;
                    coin.cmc_rank = item.cmc_rank;
                }
                else //create new
                {
                    Datum dt = new Datum();
                    dt.name = item.name;
                    dt.circulating_supply = item.circulating_supply;
                    dt.symbol = item.symbol;
                    dt.max_supply = item.max_supply;
                    dt.cmc_rank = item.cmc_rank;
                    await _context.Data.AddAsync(dt);
                }
            }
            await _context.SaveChangesAsync();
            return View(crl); 
        }

        public async Task<IActionResult> Details(string name) //from db
        {
            if(name == null) return NotFound();
            var dat = await _context.Data.FirstOrDefaultAsync(c => c.name == name);
            return View(dat);
        }

        static string makeAPICall()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "10";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());
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
