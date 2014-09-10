using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using CodingChick.UdemyUniversal.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodingChick.UdemyUniversal.Core.Base.Helpers
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }


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
