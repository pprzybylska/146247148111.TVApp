using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using _146247148111.TVApp.WebApplication.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Configuration;
using System.Diagnostics;
using System.Transactions;
using System.Xml.Linq;

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

        [HttpGet]
        public IActionResult UpdateProducer(int ID)
        {
            IEnumerable<IProducer> producers = BLC_object.GetProducers();
            var producerOld = producers.FirstOrDefault(p => p.ID == ID);
            return View(producerOld);
        }

        [HttpPost]
        public IActionResult UpdateProducer(int ID, string Name, string Country)
        {
            IProducer producer = BLC_object.UpdateProducer(ID, Name, Country);
            if (producer != null)
            {
                return RedirectToAction("ProducerCatalog");
            }

            return View();

        }

        public IActionResult TVCatalog()
        {
            IEnumerable<IProducer> producers = BLC_object.GetProducers();
            var producersNames = producers.Select(p => p.Name).ToList();
            ViewBag.ProducerNames = new SelectList(producersNames);
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
            IEnumerable<ITV> tvs = BLC_object.GetTVs();
            var tvOld = tvs.FirstOrDefault(tv => tv.ID == ID);
            ViewBag.OldProducerName = producers.FirstOrDefault(p => p.ID == tvOld?.ProducerId)?.Name;
            Console.WriteLine(ViewBag.OldProducerName);
            ViewBag.ProducerNames = new SelectList(producersNames);
            return View(tvOld);
        }

        [HttpPost]
        public IActionResult UpdateTV(int ID, string Name, string ProducerName, ScreenType Screen, int ScreenSize)
        {
            Console.WriteLine("Post" + ProducerName);
            ITV tv = BLC_object.UpdateTV(ID, Name, ProducerName, Screen, ScreenSize);

            if (tv !=  null)
            {
                return RedirectToAction("TVCatalog");
            }

            return View();
        }

        public IActionResult SearchTvs(string keywords)
        {
            IEnumerable<ITV> SearchedTVs = BLC_object.SearchTVsByKeyword(keywords);
            return View("TVCatalog", SearchedTVs);
        }

        public IActionResult FilterTVsByProducer(string producer)
        {
            IEnumerable<ITV> FilteredTVs = BLC_object.FilterByProducer(producer);
            return View("TVCatalog", FilteredTVs);
        }

        public IActionResult FilterTVsByScreenSize(int minSize, int maxSize)
        {
            IEnumerable<ITV> FilteredTVs = BLC_object.FilterByScreenSize(minSize, maxSize);
            return View("TVCatalog", FilteredTVs);
        }

        public IActionResult FilterTVsByScreenType(ScreenType screenType)
        {
            IEnumerable<ITV> FilteredTVs = BLC_object.FilterByScreenType(screenType);
            return View("TVCatalog", FilteredTVs);
        }

        public IActionResult Index()
        {
            return RedirectToAction("TVCatalog");
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