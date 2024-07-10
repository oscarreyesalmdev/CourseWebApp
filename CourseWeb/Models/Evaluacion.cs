namespace CourseWeb.Models
{
    public class Evaluacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int CursoId { get; set; }

        public string evaluacion { get; set; }

        public Curso Curso { get; set; }

    }
}
