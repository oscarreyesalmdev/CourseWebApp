using CourseWeb.Controllers;
using CourseWeb.Data;
using CourseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CourseWeb.Tests
{
    public class CourseControllerTests
    {
        private readonly DbContextOptions<CourseAppContext> _options;
        private CourseAppContext _context;

        public CourseControllerTests()
        {
            _options = new DbContextOptionsBuilder<CourseAppContext>()
                .UseInMemoryDatabase(databaseName: "CourseAppDatabase")
                .Options;

            _context = new CourseAppContext(_options);
        }

        private void SeedDatabase()
        {
            _context.Cursos.AddRange(
                new Curso { Titulo = "Curso 1", Descripcion = "Descripción 1", FechaPublicacion = System.DateTime.Now },
                new Curso { Titulo = "Curso 2", Descripcion = "Descripción 2", FechaPublicacion = System.DateTime.Now }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task CrearC_Post_ReturnsRedirectToIndex_WhenModelIsValid()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            _context = new CourseAppContext(_options);
            SeedDatabase();

            var controller = new CourseController(null, _context);

            var newCourse = new Curso
            {
                Titulo = "Nuevo Curso",
                Descripcion = "Nueva Descripción",
                FechaPublicacion = System.DateTime.Now
            };

            // Act
            var result = await controller.CrearC(newCourse);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfCourses()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            _context = new CourseAppContext(_options);
            SeedDatabase();

            var controller = new CourseController(null, _context);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Curso>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

       
    }
}
