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

        public async Task<CoursesListPage<Course>> GetCoursesOnSaleFull(int numberOfResults, int pageNumber)
        {
            var methodParams = AddAllFieldsMethodParams();
            return await _iUdemyDataManager.GetDataAsync<CoursesListPage<Course>>(string.Format("discover/on-sale/{0}/{1}", numberOfResults, pageNumber), methodParams, _iOAuthService.Token);
        }

        private List<KeyValuePair<string, string>> AddAllFieldsMethodParams()
        {
            var methodParams =
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("mobileCompatible", "2"),
                    new KeyValuePair<string, string>("fields[user]","@min,jobTitle,description,-timeZone"),
                    new KeyValuePair<string, string>("fields[course]", "@default,-settings,-avgRatingRatio,-isInstructor,-canEdit,-isPremium,-giftUrl,-isPrivate,-publishedTime,description"),
                    new KeyValuePair<string, string>("fields[asset]", "@default,-description,-thumbnailUrl,-remainingProcessingTime,-status&locale=en"),
                };
            return methodParams;
        }

        public async Task<CoursesListPage<Course>> GetCoursesNewFull(int numberOfResults, int pageNumber)
        {
            var methodParams = AddAllFieldsMethodParams();
            return await _iUdemyDataManager.GetDataAsync<CoursesListPage<Course>>(string.Format("discover/new/{0}/{1}", numberOfResults, pageNumber), methodParams, _iOAuthService.Token);
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

        public async Task<List<Chapter>> GetCourseCuriculum(string courseId)
        {
            var methodParams =
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("fields[lecture]","@min,-images,asset,-publicUrl,-isDownloadable, -ISPUBLISHED,-QUESTIONEXISTS,-SOURCECODEEXISTS,-EXTERNALLINKEXISTS,COMPLETIONRATIO,PROGRESSSTATUS"),
                    new KeyValuePair<string, string>("FIELDS[ASSET]", "@DEFAULT,DOWNLOADURL,-DESCRIPTION,-REMAININGPROCESSINGTIME"),
                };

            return await _iUdemyDataManager.GetDataAsyncWithConverter<List<Chapter>>(string.Format("courses/{0}/curriculum", courseId), methodParams, _iOAuthService.Token);
        }

        public async Task<CourseDetails> GetFullCourseCuriculum(string courseId)
        {
            var methodParams =
             new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("fields[user]","@min,jobTitle,description"),
                    new KeyValuePair<string, string>("fields[course]", "description,curriculum,faq,whatYouWillLearnData,requirementsData,whoShouldAttendData-giftUrl,-isPrivate,-isInAppPurchaseEnabled,-notifications,-inAppPurchaseEnabled,-isAvailableOnIos,-inAppPurchasePriceText,-isPremium,-canEdit,-isInstructor,-publishedTime,-isPaid,-originalPriceText,-promoAsset,-settings"),
                    new KeyValuePair<string, string>("fields[lecture]", "@min,-images,asset,-publicUrl,-isDownloadable,-isPublished,-questionExists,-sourceCodeExists,-externalLinkExists,extras,isCompleted,completionRatio"),
                    new KeyValuePair<string, string>("FIELDS[ASSET]", "@default,-description,-remainingProcessingTime,-status"),
                };

            return await _iUdemyDataManager.GetDataAsyncWithConverter<CourseDetails>(string.Format("courses/{0}", courseId), methodParams, _iOAuthService.Token);
        }

        public async Task<Success> PostLectureProgress(string lectureId, bool isFinished)
        {
            if (isFinished)
            {
                return await _iUdemyDataManager.PostDataAsync<Success>(string.Format("lectures/{0}/completed", lectureId), _iOAuthService.Token);
            }
            return await _iUdemyDataManager.PostDataAsync<Success>(string.Format("lectures/{0}/viewed", lectureId), _iOAuthService.Token);
        }

        public async Task<CoursesListPage<Course>> GetCoursesInCategory(string categoryId, int numberOfResults, int pageNumber)
        {
            var methodParams = AddAllFieldsMethodParams();
            return await _iUdemyDataManager.GetDataAsync<CoursesListPage<Course>>
                (string.Format("discover/category/{0}/{1}/{2}", categoryId, numberOfResults, pageNumber), methodParams, _iOAuthService.Token);
           
        }

        public async Task<ReviewsListPage> GetReviews(string courseId, int numberOfResults, int pageNumber)
        {
            var methodParams =
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("fields[user]", "@min,-timeZone,-url"),
                    new KeyValuePair<string, string>("p", pageNumber.ToString()),
                    new KeyValuePair<string, string>("pageSize", numberOfResults.ToString())
                };

            return
                await
                    _iUdemyDataManager.GetDataAsync<ReviewsListPage>(string.Format("courses/{0}/reviews", courseId),
                        methodParams);
        }
    }
}
