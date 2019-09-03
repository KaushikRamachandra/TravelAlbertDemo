using RestSharp;
using RestSharp.Authenticators;

namespace TravelAlberta.Exercise.WebApi.Client
{
    public class ApiTokenAuthenticator : IAuthenticator
    {
        public string Token { get; private set; }

        public ApiTokenAuthenticator(string token)
        {
            Token = token;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            client.AddDefaultHeader("Authorization", string.Format(@"Bearer {0}", Token));
        }
    }
}
