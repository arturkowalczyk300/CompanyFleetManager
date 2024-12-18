namespace CompanyFleetManagerWebApp.Models
{
    public class UserLoggedState
    {
        public bool IsUserLogged { get; set; } = false;
        public string LoggedUserEmail { get; set; } = string.Empty;
    }
}
