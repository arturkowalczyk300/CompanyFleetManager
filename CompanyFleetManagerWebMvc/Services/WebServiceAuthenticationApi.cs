
using Microsoft.AspNetCore.Identity.Data;

namespace CompanyFleetManagerWebApp.Services
{
    public class WebServiceAuthenticationApi
    {
        private readonly HttpClient _httpClient;

        public WebServiceAuthenticationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        internal async Task<LoginResult> LoginAsync(string email, string password)
        {
            var result = await _httpClient.PostAsJsonAsync("api/users/login", new LoginRequest { Email = email, Password = password });

            if (result.IsSuccessStatusCode)
            {
                var loginResult = await result.Content.ReadFromJsonAsync<LoginResult>();
                return loginResult;
            }

            return new LoginResult
            {
                IsSuccess = false,
                ErrorMessage = "Invalid email or password."
            };
        }

        internal async Task<bool> LogoutAsync()
        {
            var result = await _httpClient.PostAsync("api/users/logout", null);

            return result.IsSuccessStatusCode;
        }

        internal async Task<AuthStatus> GetAuthStatusAsync()
        {
            var result = await _httpClient.GetAsync("api/users/status");

            if (result.IsSuccessStatusCode)
            {
                var authStatus = await result.Content.ReadFromJsonAsync<AuthStatus>();
                return authStatus ?? new AuthStatus { IsAuthenticated = false };
            }

            return new AuthStatus
            {
                IsAuthenticated = false
            };
        }

        public class AuthStatus
        {
            public bool IsAuthenticated { get; set; }
            public string Username { get; set; }
        }
    }
}
