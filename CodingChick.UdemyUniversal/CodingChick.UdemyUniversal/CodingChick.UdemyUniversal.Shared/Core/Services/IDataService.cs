using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public interface IDataService
    {
        Task<CoursesListPage<Course>> GetCoursesOnSaleFull();
        Task<CoursesListPage<Course>> GetCoursesNewBasic(int numberOfResults, int pageNumber);
        Task<CoursesListPage<Course>> GetCoursesOnSaleBasic(int numberOfResults, int pageNumber);
        Task<List<Category>> GetCategories();
        Task<CoursesListPage<MyCourse>> GetMyCourses();
    }
}