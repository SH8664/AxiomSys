using System.Net.Http.Headers;
namespace AxiomSysPortal.Services
{

    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;
        public BearerTokenHandler(IHttpContextAccessor accessor) => _accessor = accessor;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var token = _accessor.HttpContext?.User.FindFirst("access_token")?.Value;
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, ct);
        }
    }
}
