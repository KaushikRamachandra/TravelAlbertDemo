using RestSharp;
using System;
using System.Threading.Tasks;

namespace TravelAlberta.Exercise.WebApi.Client
{
    public interface ICsvContentClient
    {
        Task<string> GetCsvContent();
    }

    public class CsvContentClient : ClientBase, ICsvContentClient
    {

        public CsvContentClient(string url, string token)
            : base(url, token)
        {

        }

        public async Task<string> GetCsvContent()
        {
            RestClient client = new RestClient(base._url);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            IRestResponse<object> csvObject = await client.ExecuteGetTaskAsync<object>(request).ConfigureAwait(false);
            return csvObject.Content;
        }
    }
}
