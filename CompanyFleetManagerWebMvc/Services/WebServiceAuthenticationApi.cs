
using CompanyFleetManagerWebApp.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace CompanyFleetManagerWebApp.Services
{
    public class WebServiceAuthenticationApi
    {
        private readonly HttpClient _httpClient;
        private readonly UserLoggedState _userLoggedState;

        public WebServiceAuthenticationApi(HttpClient httpClient, UserLoggedState userLoggedState)
        {
            _httpClient = httpClient;
            _userLoggedState = userLoggedState;
        }

        internal async Task<LoginResult> LoginAsync(string email, string password)
        {
            var result = await _httpClient.PostAsJsonAsync("login", new LoginRequest { Email = email, Password = password });

            if (result.IsSuccessStatusCode)
            {
                var loginResult = new LoginResult() { IsSuccess = true };
                _userLoggedState.LoggedUserEmail = email;
                _userLoggedState.IsUserLogged = true;
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
            var result = await _httpClient.PostAsync("logout", null);

            if (result.IsSuccessStatusCode)
            {
                _userLoggedState.IsUserLogged = false;
                _userLoggedState.LoggedUserEmail = string.Empty;
                return true;
            }

            return false; //logout failed
        }

        internal async Task<UserLoggedState> RetrieveAuthStatusAsync()
        {
            var result = await _httpClient.GetAsync("status");

            if (result.IsSuccessStatusCode)
            {
                var authStatus = await result.Content.ReadFromJsonAsync<UserLoggedState>();

                if(authStatus != null)
                {
                _userLoggedState.IsUserLogged = authStatus.IsUserLogged;
                _userLoggedState.LoggedUserEmail = authStatus.LoggedUserEmail;
                }

                return authStatus ?? new UserLoggedState { IsUserLogged = false };
            }

            return new UserLoggedState
            {
                IsUserLogged = false
            };
        }
    }
}
