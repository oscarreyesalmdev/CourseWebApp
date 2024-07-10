using CourseWeb.Data;
using CourseWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseWeb.Controllers
{
    public class EvaluacionController : Controller
    {
        private readonly ILogger<EvaluacionController> _logger;
        private readonly CourseAppContext _context;
        public EvaluacionController(ILogger<EvaluacionController> logger, CourseAppContext context)
        {
            _logger = logger;
            _context = context;
        }
        // GET: EvaluacionController
        public ActionResult Index()
        {
            var evaluacion = _context.evaluacion.
                Include(e => e.Curso)
                .ToList();
            return View(evaluacion);
        }

        public ActionResult CrearE()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CrearE(Evaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluacion);
        }

        // GET: EvaluacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EvaluacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        
    }
}
