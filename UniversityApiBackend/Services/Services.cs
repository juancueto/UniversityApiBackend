using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class Services
    {
        private readonly UniversityDBContext _context;

        public Services(UniversityDBContext context)
        {
            _context = context;
        }

        // BUSCAR USUARIOS POR EMAIL
        public IEnumerable<User> BuscarUsuariosPorEmail(string email)
        {
            return this._context.Users.Where(usuario => usuario.Email.Contains(email));
        }

        // BUSCAR ALUMNOS MAYORES DE EDAD
        public IEnumerable<Student> BuscarAlumnosMayoresEdad()
        {
            int currentYear = DateTime.Now.Year;
            return this._context.Students.Where(student => (currentYear - student.Dob.Year) >= 18);
        }

        // BUSCAR ALUMNOS QUE TENGAN AL MENOS UN CURSO
        public IEnumerable<Student> BuscarAlumnosConAlMenosUnCurso()
        {
            return this._context.Students.Where(student => student.Courses.Count > 0);
        }

        // BUSCAR CURSOS DE UN NIVEL DETERMINADO QUE AL MENOS TENGAN UN ALUMNO INSCRITO
        public IEnumerable<Course> BuscarCursoPorNivelConAlMenosUnAlumno(Level level)
        {
            return this._context.Courses.Where(course => course.Level == level && course.Students.Count > 0);
        }

        // BUSCAR CURSOS DE UN NIVEL DETERMINADO QUE SEAN DE UNA CATEGORIA DETERMINADA
        public IEnumerable<Course> BuscarCursoPorNivelYCategoria(Level level, int idCategoria)
        {
            return this._context.Courses.Where(course => course.Level == level && course.Categories.Any(category => category.Id == idCategoria));
        }

        // BUSCAR CURSOS SIN ALUMNOS
        public IEnumerable<Course> BuscarCursosSinAlumnos()
        {
            return this._context.Courses.Where(course => course.Students.Count == 0);
        }
    }
}
