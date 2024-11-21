
using CompanyFleetManager.Models.Entities;

namespace CompanyFleetManagerWebApp.Services
{
    public class WebServiceFleetApi
    {
        private readonly HttpClient _httpClient;

        public WebServiceFleetApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //employees
        internal async Task<List<Employee>>? FetchEmployees()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("employees");
        }
        internal async Task AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var response = await _httpClient.PostAsJsonAsync("employees", employee);
            response.EnsureSuccessStatusCode();
        }
        internal async Task UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var response = await _httpClient.PutAsJsonAsync("employees", employee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task RemoveEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var response = await _httpClient.DeleteAsync($"employees/{employee.EmployeeId}");
            response.EnsureSuccessStatusCode();
        }

        // vehicles
        internal async Task<List<Vehicle>>? FetchVehicles()
        {
            return await _httpClient.GetFromJsonAsync<List<Vehicle>>("vehicles");
        }

        internal async Task AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            var response = await _httpClient.PostAsJsonAsync("vehicles", vehicle);
            response.EnsureSuccessStatusCode();
        }
        internal async Task UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            var response = await _httpClient.PutAsJsonAsync("vehicles", vehicle);
            response.EnsureSuccessStatusCode();
        }

        internal async Task RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            var response = await _httpClient.DeleteAsync($"vehicles/{vehicle.VehicleId}");
            response.EnsureSuccessStatusCode();
        }

        // rentals
        internal async Task<List<Rental>>? FetchRentals()
        {
            return await _httpClient.GetFromJsonAsync<List<Rental>>("rentals");
        }
        internal async Task AddRental(Rental rental)
        {
            if (rental == null)
            {
                throw new ArgumentNullException(nameof(rental));
            }

            var response = await _httpClient.PostAsJsonAsync("rentals", rental);
            response.EnsureSuccessStatusCode();
        }
        internal async Task UpdateRental(Rental rental)
        {
            if (rental == null)
            {
                throw new ArgumentNullException(nameof(rental));
            }

            var response = await _httpClient.PutAsJsonAsync("rentals", rental);
            response.EnsureSuccessStatusCode();
        }

        internal async Task RemoveRental(Rental rental)
        {
            if (rental == null)
            {
                throw new ArgumentNullException(nameof(rental));
            }

            var response = await _httpClient.DeleteAsync($"rentals/{rental.RentalId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
