using CourseWeb.Data;
using CourseWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace CourseWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseAppContext _context;
        public CourseController(ILogger<CourseController> logger, CourseAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: CourseController
        public ActionResult Index()
        {
            var curso = _context.Cursos.ToList();
            return View(curso);
        }

        public ActionResult CursoUser()
        {
            var curso = _context.Cursos.ToList();
            return View(curso);
        }

        public ActionResult EditarC()
        {
            return View();
        }

        public ActionResult CrearC()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CrearC(Curso curso)
        {
            if (ModelState.IsValid)
            {
                if (curso.Titulo.Length > 40)
                {
                    ModelState.AddModelError("Titulo", "El título no puede tener más de 40 caracteres.");
                    return View(curso);
                }

                if (curso.Descripcion != null && curso.Descripcion.Length > 300)
                {
                    ModelState.AddModelError("Descripcion", "La descripción no puede tener más de 300 caracteres.");
                    return View(curso);
                }

                if (curso.FechaPublicacion == default(DateTime))
                {
                    ModelState.AddModelError("FechaPublicacion", "La fecha de publicación es obligatoria.");
                    return View(curso);
                }

                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        [HttpGet]
        public async Task<ActionResult> EditarC(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        // POST: CourseController/EditarC/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarC(int id, [Bind("Id,Titulo,Descripcion,FechaPublicacion")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (curso.Titulo.Length > 40)
                {
                    ModelState.AddModelError("Titulo", "El título no puede tener más de 40 caracteres.");
                    return View(curso);
                }

                if (curso.Descripcion != null && curso.Descripcion.Length > 300)
                {
                    ModelState.AddModelError("Descripcion", "La descripción no puede tener más de 300 caracteres.");
                    return View(curso);
                }

                if (curso.FechaPublicacion == default(DateTime))
                {
                    ModelState.AddModelError("FechaPublicacion", "La fecha de publicación es obligatoria.");
                    return View(curso);
                }

                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(curso);
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("EliminarC")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarC(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
