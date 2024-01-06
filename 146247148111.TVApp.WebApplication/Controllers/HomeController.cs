using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using _146247148111.TVApp.WebApplication.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Configuration;
using System.Diagnostics;
using System.Transactions;

namespace _146247148111.TVApp.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BLC.BLC BLC_object { get; set; }
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            string execPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            Console.WriteLine(execPath);
            _configuration = configuration;
            string libraryName = _configuration["DAOLibraryName"];
            Console.WriteLine(libraryName);

            BLC_object = new BLC.BLC(libraryName);

            _logger = logger;
        }

        public IActionResult ProducerCatalog()
        {
            IEnumerable<IProducer> producers = BLC_object.GetProducers();
            return View(producers);
        }

        [HttpGet]
        public IActionResult CreateProducer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProducer(string Name, string Country)
        {
            IProducer producer = BLC_object.CreateNewProducer(Name, Country);

            if (producer != null)
            {
                return RedirectToAction("ProducerCatalog");
            }

            return View();
        }

        public IActionResult DeleteProducer(int ID)
        {
            BLC_object.DeleteProducerById(ID);
            return RedirectToAction("ProducerCatalog");
        }

        public IActionResult TVCatalog()
        {
            IEnumerable<ITV> TVs = BLC_object.GetTVs();
            return View(TVs);
        }

        [HttpGet]
        public IActionResult CreateTV()
        {
            IEnumerable<IProducer> producers = BLC_object.GetProducers();
            var producersNames = producers.Select(p => p.Name).ToList();
            ViewBag.ProducerNames = new SelectList(producersNames);

            return View();
        }

        [HttpPost]
        public IActionResult CreateTV(string Name, string ProducerName, int ScreenSize, ScreenType Screen)
        {
            ITV tv = BLC_object.CreateNewTV(Name, ProducerName, Screen, ScreenSize);

            if (tv !=  null) { 
                return RedirectToAction("TVCatalog");
            }

            return View();
        }

        public IActionResult DeleteTV(int ID)
        {
            BLC_object.DeleteTVById(ID);
            return RedirectToAction("TVCatalog");
        }

        [HttpGet]
        public IActionResult UpdateTV(int ID)
        {
            IEnumerable<IProducer> producers = BLC_object.GetProducers();
            var producersNames = producers.Select(p => p.Name).ToList();
            ViewBag.ProducerNames = new SelectList(producersNames);
            return View();
        }

        public IActionResult Index()
        {
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