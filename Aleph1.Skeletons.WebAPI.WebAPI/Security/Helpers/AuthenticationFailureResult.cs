using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security.Helpers
{
    internal class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };
            return Task.FromResult(response);
        }
    }
}