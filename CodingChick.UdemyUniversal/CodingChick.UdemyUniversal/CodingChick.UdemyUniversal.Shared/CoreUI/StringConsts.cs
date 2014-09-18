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
            {"Default", String.Format("{0}/Default.png", DefaultCategoriesPath)},
            {"Business", String.Format("{0}/Business.png", DefaultCategoriesPath)},
            {"Design", String.Format("{0}/Design.png", DefaultCategoriesPath)},
            {"Development", String.Format("{0}/Development.png", DefaultCategoriesPath)},
            {"Health & Fitness", String.Format("{0}/Health&Fitness.png", DefaultCategoriesPath)},
            {"IT & Software", String.Format("{0}/IT&Software.png", DefaultCategoriesPath)},
            {"Lifestyle", String.Format("{0}/Lifestyle.png", DefaultCategoriesPath)},
            {"Marketing", String.Format("{0}/Marketing.png", DefaultCategoriesPath)},
            {"Music", String.Format("{0}/Music.png", DefaultCategoriesPath)},
            {"Office Productivity", String.Format("{0}/OfficeProductivity.png", DefaultCategoriesPath)},
            {"Personal Development", String.Format("{0}/PersonalDevelopment.png", DefaultCategoriesPath)},
            {"Social Science", String.Format("{0}/SocialScience.png", DefaultCategoriesPath)},
            {"Photography", String.Format("{0}/Photography.png", DefaultCategoriesPath)},
            {"Teacher Training", String.Format("{0}/TeacherTraining.png", DefaultCategoriesPath)},
            {"Humanities", String.Format("{0}/Humanities.png", DefaultCategoriesPath)},
            {"Test Prep", String.Format("{0}/TestPrep.png", DefaultCategoriesPath)},
            {"Math & Science", String.Format("{0}/Math&Science.png", DefaultCategoriesPath)},
            {"Language", String.Format("{0}/Language.png", DefaultCategoriesPath)}
        };
    }
}
