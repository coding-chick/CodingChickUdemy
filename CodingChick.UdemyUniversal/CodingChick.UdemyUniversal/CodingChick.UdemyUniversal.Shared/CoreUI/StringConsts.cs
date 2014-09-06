using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChick.UdemyUniversal.CoreUI
{
    public class StringConsts
    {
        public const string DefaultCategoriesPath = "ms-appx:/Assets/Categories";

        public static Dictionary<string, string> CategoriesUrls = new Dictionary<string, string>()
        {
            {"Default", string.Format("{0}/Default.png", DefaultCategoriesPath)},
            {"Business", string.Format("{0}/Business.png", DefaultCategoriesPath)},
            {"Design", string.Format("{0}/Design.png", DefaultCategoriesPath)},
            {"Development", string.Format("{0}/Development.png", DefaultCategoriesPath)},
            {"Health & Fitness", string.Format("{0}/Health&Fitness.png", DefaultCategoriesPath)},
            {"IT & Software", string.Format("{0}/IT&Software.png", DefaultCategoriesPath)},
            {"Lifestyle", string.Format("{0}/Lifestyle.png", DefaultCategoriesPath)},
            {"Marketing", string.Format("{0}/Marketing.png", DefaultCategoriesPath)},
            {"Music", string.Format("{0}/Music.png", DefaultCategoriesPath)},
            {"Office Productivity", string.Format("{0}/OfficeProductivity.png", DefaultCategoriesPath)},
            {"Personal Development", string.Format("{0}/PersonalDevelopment.png", DefaultCategoriesPath)},
            {"Social Science", string.Format("{0}/SocialScience.png", DefaultCategoriesPath)},
            {"Photography", string.Format("{0}/Photography.png", DefaultCategoriesPath)},
            {"Teacher Training", string.Format("{0}/TeacherTraining.png", DefaultCategoriesPath)},
            {"Humanities", string.Format("{0}/Humanities.png", DefaultCategoriesPath)},
            {"Test Prep", string.Format("{0}/TestPrep.png", DefaultCategoriesPath)},
            {"Math & Science", string.Format("{0}/Math&Science.png", DefaultCategoriesPath)},
            {"Language", string.Format("{0}/Language.png", DefaultCategoriesPath)}
        }; 
    }
}
