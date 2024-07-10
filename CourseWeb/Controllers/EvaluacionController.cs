using CourseWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var evaluacion = _context.Evaluacion.ToList();
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
