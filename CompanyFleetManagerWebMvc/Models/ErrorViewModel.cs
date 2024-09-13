namespace CompanyFleetManagerWebMvc.Models
{
    public class ErrorViewModel
    {
        public string? DetailedMessage { get; set; }

        public bool ShowDetailedMessage => !string.IsNullOrEmpty(DetailedMessage);
    }
}
