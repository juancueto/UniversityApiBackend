using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly UniversityDBContext _context;
        public IEnumerable<Course> GetAllCoursesByCategoryId(int idCategory)
        {
            return null; // _context.Courses.Where(p => p.Categories.Any(q => q.Id == idCategory));
        }
    }
}
