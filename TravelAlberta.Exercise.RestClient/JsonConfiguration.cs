using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization.Formatters;

namespace TravelAlberta.Exercise.WebApi.Client
{
    public class JsonConfiguration
    {
        private static JsonSerializerSettings _settings;
        public static JsonSerializerSettings Settings
        {
            get { return _settings ?? (_settings = DefaultSettings()); }
            set { _settings = value; }
        }

        /// <summary>
        /// Default JSON serializer settings for parsing response bodies.
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerSettings DefaultSettings()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
                //ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            jsonSettings.Converters.Add(new IsoDateTimeConverter());

            return jsonSettings;
        }
    }
}
