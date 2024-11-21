namespace CompanyFleetManagerWebApp.Services
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; } = false;
        public string? ErrorMessage { get; set; } = null;

    }
}