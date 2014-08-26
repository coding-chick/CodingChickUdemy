using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public interface IDataService
    {
        Task<CoursesListPage> GetCoursesOnSaleFull();
        Task<CoursesListPage> GetCoursesNewBasic(int numberOfResults, int pageNumber);
        Task<CoursesListPage> GetCoursesOnSaleBasic();
        Task<List<Category>> GetCategories();
    }
}