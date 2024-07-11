using CourseWeb.Controllers;
using CourseWeb.Data;
using CourseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class CourseControllerTests
{
    private readonly Mock<ILogger<CourseController>> _loggerMock;

    public CourseControllerTests()
    {
        _loggerMock = new Mock<ILogger<CourseController>>();
    }

    private CourseAppContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<CourseAppContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a new database instance for each test
            .Options;

        return new CourseAppContext(options);
    }

    [Fact]
    public void Index_ReturnsViewResult_WithListOfCourses()
    {
        // Arrange
        var context = GetInMemoryContext();
        var cursos = new List<Curso>
        {
            new Curso { Id = 1, Titulo = "Curso 1", Descripcion = "Descripcion 1", FechaPublicacion = DateTime.Now },
            new Curso { Id = 2, Titulo = "Curso 2", Descripcion = "Descripcion 2", FechaPublicacion = DateTime.Now }
        };
        context.Cursos.AddRange(cursos);
        context.SaveChanges();

        var controller = new CourseController(_loggerMock.Object, context);

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Curso>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count);
    }

    [Fact]
    public async Task CrearC_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var context = GetInMemoryContext();
        var curso = new Curso { Titulo = "Curso 1", Descripcion = "Descripcion 1", FechaPublicacion = DateTime.Now };
        var controller = new CourseController(_loggerMock.Object, context);

        // Act
        var result = await controller.CrearC(curso);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal(1, context.Cursos.Count());
        Assert.Equal(curso, context.Cursos.First());
    }

    [Fact]
    public async Task CrearC_InvalidModel_ReturnsViewWithModelError()
    {
        // Arrange
        var context = GetInMemoryContext();
        var curso = new Curso { Titulo = new string('A', 41), Descripcion = "Descripcion 1", FechaPublicacion = DateTime.Now };
        var controller = new CourseController(_loggerMock.Object, context);
        controller.ModelState.AddModelError("Titulo", "El título no puede tener más de 40 caracteres.");

        // Act
        var result = await controller.CrearC(curso);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Curso>(viewResult.ViewData.Model);
        Assert.Equal(curso, model);
        Assert.True(controller.ModelState.ContainsKey("Titulo"));
    }

    [Fact]
    public async Task EditarC_GetValidId_ReturnsViewWithCourse()
    {
        // Arrange
        var context = GetInMemoryContext();
        var curso = new Curso { Id = 1, Titulo = "Curso 1", Descripcion = "Descripcion 1", FechaPublicacion = DateTime.Now };
        context.Cursos.Add(curso);
        context.SaveChanges();
        var controller = new CourseController(_loggerMock.Object, context);

        // Act
        var result = await controller.EditarC(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Curso>(viewResult.ViewData.Model);
        Assert.Equal(curso, model);
    }

    [Fact]
    public async Task EliminarC_ValidId_RedirectsToIndex()
    {
        // Arrange
        var context = GetInMemoryContext();
        var curso = new Curso { Id = 1, Titulo = "Curso 1", Descripcion = "Descripcion 1", FechaPublicacion = DateTime.Now };
        context.Cursos.Add(curso);
        context.SaveChanges();
        var controller = new CourseController(_loggerMock.Object, context);

        // Act
        var result = await controller.EliminarC(1);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Empty(context.Cursos);
    }
}
