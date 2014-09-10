using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public interface IDataService
    {
        Task<CoursesListPage<Course>> GetCoursesOnSaleFull(int numberOfResults, int pageNumber);
        Task<CoursesListPage<Course>> GetCoursesNewFull(int numberOfResults, int pageNumber);
        Task<List<Category>> GetCategories();
        Task<CoursesListPage<MyCourse>> GetMyCourses();
        Task<List<Chapter>> GetCourseCuriculum(string courseId);
    }
}