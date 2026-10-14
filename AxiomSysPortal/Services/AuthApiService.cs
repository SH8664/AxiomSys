using AxiomSysPortal.Models;

namespace AxiomSysPortal.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _http;
        public AuthApiService(HttpClient http) => _http = http;

        public async Task<(bool ok, string? error)> RegisterAsync(UserRegisterDto dto)
        {
            var res = await _http.PostAsJsonAsync("Auth/register", dto);
            if (res.IsSuccessStatusCode) return (true, null);

            var body = await res.Content.ReadAsStringAsync();
            return (false, string.IsNullOrWhiteSpace(body) ? "Registration failed" : body);
        }

        public async Task<(string? token, string? error)> LoginAsync(UserLoginDto dto)
        {
            var res = await _http.PostAsJsonAsync("Auth/login", dto);
            var body = (await res.Content.ReadAsStringAsync()).Trim().Trim('"');

            if (res.IsSuccessStatusCode)
                return (body, null);

            return (null, "Invalid email or password");
        }
    }
}
