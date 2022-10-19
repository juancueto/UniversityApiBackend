using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetAllCoursesByCategoryId(int idCategory);
    }
}
