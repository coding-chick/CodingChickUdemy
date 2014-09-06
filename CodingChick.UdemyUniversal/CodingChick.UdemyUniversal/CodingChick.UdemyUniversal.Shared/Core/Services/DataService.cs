using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingChick.UdemyUniversal.Core.Base;
using CodingChick.UdemyUniversal.Models;

namespace CodingChick.UdemyUniversal.Core.Services
{
    public class DataService : IDataService
    {
        private readonly IUdemyDataManager _iUdemyDataManager;
        private readonly IOAuthService _iOAuthService;

        public DataService(IUdemyDataManager iUdemyDataManager, IOAuthService iOAuthService)
        {
            _iUdemyDataManager = iUdemyDataManager;
            _iOAuthService = iOAuthService;
        }

        public async Task<CoursesListPage<Course>> GetCoursesOnSaleFull()
        {
            var methodParams =
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("mobileCompatible", "2"),
                new KeyValuePair<string, string>("fields[user]",
                    "=@min,jobTitle,description,-timeZone&&fields[course]=@default,-settings,-avgRatingRatio,-isInstructor,-canEdit,-isPremium,-giftUrl,-isPrivate,-publishedTime,description&fields[asset]=@default,-description,-thumbnailUrl,-remainingProcessingTime,-status&locale=en")
            };
            return await _iUdemyDataManager.GetDataAsync<CoursesListPage<Course>>("discover/on-sale/12/1", methodParams, _iOAuthService.Token);
        }


        public async Task<CoursesListPage<Course>> GetCoursesOnSaleBasic(int numberOfResults, int pageNumber)
        {
            var methodParams =
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("mobileCompatible", "2"),
            };
            return await _iUdemyDataManager.GetDataAsync<CoursesListPage<Course>>(string.Format("discover/on-sale/{0}/{1}", numberOfResults, pageNumber), methodParams, _iOAuthService.Token);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _iUdemyDataManager.GetDataAsync<List<Category>>("discover/categories", new List<KeyValuePair<string, string>>(), _iOAuthService.Token);

        }

        public async Task<CoursesListPage<MyCourse>> GetMyCourses()
        {
            return
                await
                    _iUdemyDataManager.GetDataAsync<CoursesListPage<MyCourse>>("users/me/taking",
                        new List<KeyValuePair<string, string>>(), _iOAuthService.Token);
        }

        public async Task<CoursesListPage<Course>> GetCoursesNewBasic(int numberOfResults, int pageNumber)
        {
            var methodParams =
            new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("mobileCompatible", "2"),
            };

            return await _iUdemyDataManager.GetDataAsync<CoursesListPage<Course>>(string.Format("discover/new/{0}/{1}", numberOfResults, pageNumber), methodParams, _iOAuthService.Token);
        }
    }
}
