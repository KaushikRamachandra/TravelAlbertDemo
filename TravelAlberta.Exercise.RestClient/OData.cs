using Newtonsoft.Json;

namespace TravelAlberta.Exercise.WebApi.Client
{
    public class OData<T>
    {
        [JsonProperty("odata.context")]
        public string Metadata { get; set; }
        public T value { get; set; }
    }
}
