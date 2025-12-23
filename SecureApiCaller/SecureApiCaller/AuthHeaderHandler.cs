using Microsoft.Extensions.Configuration;

namespace SecureApiCaller
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IConfiguration _config;

        public AuthHeaderHandler(IConfiguration config)
        {
            _config = config;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = _config["api-token-kwc"];
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }

}
