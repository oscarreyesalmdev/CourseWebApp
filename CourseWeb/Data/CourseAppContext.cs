using System.Collections.Generic;
using CourseWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWeb.Data
{
    public class CourseAppContext : DbContext
    {
            public CourseAppContext(DbContextOptions<CourseAppContext> options) : base(options)
            {

            }

            public DbSet<Usuario> Usuario { get; set; }
            public DbSet<Curso> Cursos { get; set; }
            public DbSet<Evaluacion> evaluacion { get; set; }

    }
}
