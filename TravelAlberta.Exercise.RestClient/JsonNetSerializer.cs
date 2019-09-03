using Newtonsoft.Json;
using RestSharp.Serializers;
using System.IO;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TravelAlberta.Exercise.WebApi.Client
{
    public class JsonNetSerializer : ISerializer
    {
        private readonly JsonSerializer _serializer;


        public JsonNetSerializer()
            : this(JsonConfiguration.Settings)
        {
        }

        public JsonNetSerializer(JsonSerializerSettings settings)
        {
            ContentType = "application/json";
            _serializer = JsonSerializer.Create(settings);
        }


        /// <summary>
        /// Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        /// <summary>
        /// Content type for serialized content.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Unused for JSON
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Unused for JSON
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Unused for JSON
        /// </summary>
        public string DateFormat { get; set; } 
    }
}
