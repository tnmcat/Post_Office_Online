
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PostOffice.API.DTOs;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.Client.Models;
namespace Client.Controllers
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
            return View();
        }         

        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Service()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ServiceDetail()
        {
            return View();
        }
        public IActionResult ExpressService()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult FinacialDetail()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult MoneyService()
        {
            return View();
        }
        [HttpGet]
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