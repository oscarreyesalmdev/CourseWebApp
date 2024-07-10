using System.ComponentModel.DataAnnotations;

namespace CourseWeb.Models
{
    public class Curso
    {
        public int Id { get; set; }


        [Required]
        [StringLength(100)]

        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]

        public string Descripcion { get; set; }

        public DateTime FechaPublicacion { get; set;}
    }
}
