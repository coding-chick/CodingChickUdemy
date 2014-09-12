using System;
using CodingChick.UdemyUniversal.Models;
using Newtonsoft.Json.Linq;

namespace CodingChick.UdemyUniversal.Core.Base.Helpers
{
    public class BaseDataConverter : JsonCreationConverter<Chapter>
    {
        protected override Chapter Create(Type objectType, JObject jObject)
        {
            if (FieldExists("lectureIndex", jObject))
            {
                return new Lecture();
            }
            else
            {
                return new Chapter();
            }
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}