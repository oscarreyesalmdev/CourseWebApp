using CourseWeb.Data;
using CourseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CourseAppContext _context;

        public HomeController(ILogger<HomeController> logger, CourseAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Course() 
        { 
           var curso = _context.Cursos.ToList();
            return View(curso);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Usuario()
        {
            return View();
        }
        public IActionResult Evaluacion()
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
