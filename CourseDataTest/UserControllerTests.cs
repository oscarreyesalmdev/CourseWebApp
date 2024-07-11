using CourseWeb.Controllers;
using CourseWeb.Data;
using CourseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CourseWeb.Tests
{
    public class UserControllerTests
    {
        private readonly DbContextOptions<CourseAppContext> _options;
        private CourseAppContext _context;

        public UserControllerTests()
        {
            _options = new DbContextOptionsBuilder<CourseAppContext>()
                .UseInMemoryDatabase(databaseName: "CourseAppDatabase")
                .Options;

            _context = new CourseAppContext(_options);
        }

        private void SeedDatabase()
        {
            _context.Usuario.AddRange(
                new Usuario { Nombre = "Usuario 1", Telefono = "1234567890", email = "usuario1@example.com" },
                new Usuario { Nombre = "Usuario 2", Telefono = "0987654321", email = "usuario2@example.com" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            _context = new CourseAppContext(_options);
            SeedDatabase();

            var controller = new UserController(null, _context);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            _context = new CourseAppContext(_options);
            SeedDatabase();

            var controller = new UserController(null, _context);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Usuario>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}



