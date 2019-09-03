using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace TravelAlberta.Exercise.WebApi.Client
{
    public class ClientBase
    {
        protected readonly IAuthenticator _auth;
        protected readonly string _url;

        public ClientBase(string url, string apikey)
        {
            _url = url;
            _auth = new ApiTokenAuthenticator(apikey);
        }

        public ClientBase(string url, IAuthenticator auth)
        {
            _url = url;
            _auth = auth;
        }

        /// <summary>
        ///     Returns a RestClient instance with the appropriate authentication headers.
        /// </summary>
        /// <returns></returns>
        public RestClient GetClient()
        {
            var client = new RestClient(_url);
            client.Authenticator = _auth;
            client.Timeout = 120000;
            return client;
        }

        /// <summary>
        ///     Execute a given query and attempt to deserialise the results as JSON, to type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = GetClient().Execute(request);
            HandleHttpStatusCode(response);

            // parse response using Json.Net
            return JsonConvert.DeserializeObject<T>(response.Content, JsonConfiguration.Settings);
        }

        /// <summary>
        ///     Execute a given query and ensure that it completed successfully
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IRestResponse Execute(RestRequest request)
        {
            var response = GetClient().Execute(request);
            HandleHttpStatusCode(response);
            return response;
        }

        public Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            GetClient().ExecuteAsync(request, response =>
            {
                var exception = ErrorException(response);
                if (exception != null)
                {
                    taskCompletionSource.TrySetException(exception);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<T>(response.Content, JsonConfiguration.Settings);
                    taskCompletionSource.TrySetResult(obj);
                }
            });

            return taskCompletionSource.Task;
        }

        public Task<IRestResponse> ExecuteAsync(RestRequest request)
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            GetClient().ExecuteAsync(request, response =>
            {
                var exception = ErrorException(response);
                if (exception != null)
                    taskCompletionSource.TrySetException(exception);
                else
                    taskCompletionSource.TrySetResult(response);
            });

            return taskCompletionSource.Task;
        }

        public Task<T> ExecuteAsync<T>(RestRequest request, Action<IRestResponse, TaskCompletionSource<T>> action)
            where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            GetClient().ExecuteAsync(request, response =>
            {
                var exception = ErrorException(response);
                if (exception != null)
                    taskCompletionSource.TrySetException(exception);
                else
                    action.Invoke(response, taskCompletionSource);
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        ///     Ensures that the response returned a valid HTTP status code and was excuted
        ///     without errors.
        /// </summary>
        /// <param name="response"></param>
        private static void HandleHttpStatusCode(IRestResponse response)
        {
            var exception = ErrorException(response);
            if (exception != null)
                throw exception;
        }

        protected static System.Exception ErrorException(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response. Check inner details.";
                return new ApplicationException(message, response.ErrorException);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new SecurityException("Web Api threw an Unauthorized exception.");

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                if (string.IsNullOrWhiteSpace(response.StatusDescription))
                    return new SecurityException("Web Api threw a Forbidden exception.");
                else
                    return new SecurityException("Web Api threw a Forbidden exception. " + response.StatusDescription);
            }

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return new ApplicationException("Something went wrong on the Web Api. Web Api failed to complete the operation you have requested for.");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return new Exception("The object you were looking for was not found.");

            if (response.StatusCode == HttpStatusCode.Conflict)
                return new Exception("A duplicate was found!");

            return null;
        }

        private static string EmbeddedErrorMessage(IRestResponse response /*string responseExceptionMessage*/)
        {
            if (string.IsNullOrEmpty(response.Content))
                return "Unknown error.";

            var errorObject = JsonConvert.DeserializeObject<EmbeddedException>(response.Content);

            if (errorObject.ExceptionType.Equals("GeoTrac.App.Operations.WebApi.UnauthorizedEntityException", StringComparison.CurrentCultureIgnoreCase))
                return string.Format("Unauthorized access to Geotrac entity. ( {0} )", errorObject.ExceptionMessage);

            // return the original message
            return errorObject.ExceptionMessage;
        }
    }

    internal class EmbeddedException
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
    }
}
