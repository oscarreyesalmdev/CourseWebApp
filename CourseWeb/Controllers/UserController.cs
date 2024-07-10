using CourseWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly CourseAppContext _context;
        public UserController(ILogger<UserController> logger, CourseAppContext context)
        {
            _logger = logger;
            _context = context;
        }
        // GET: UserController
        public ActionResult Index()
        {
            var user = _context.Usuario.ToList();
            return View(user);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

       
    }
}
