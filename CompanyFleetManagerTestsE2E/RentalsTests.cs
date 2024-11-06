using Microsoft.Playwright;

namespace CompanyFleetManagerTestsE2E
{
    public class RentalsTests : IAsyncLifetime
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
            await _page.GotoAsync("https://localhost:7079");
        }

        public async Task DisposeAsync()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Fact]
        public async Task RentalsTest_OnLoad_ShouldDisplayRentals()
        {
            await _page.ClickAsync("#PageRentals");

            var rows = await _page.QuerySelectorAllAsync("table.table tbody tr");
            Assert.NotEmpty(rows);

            var firstRowCells = await rows.First().QuerySelectorAllAsync("td");
            var rentedVehicleText = await firstRowCells[0].InnerTextAsync();
            var rentingEmployeeText = await firstRowCells[1].InnerTextAsync();

            Assert.Equal("Audi A6, DW 321AB", rentedVehicleText);
            Assert.Equal("Janusz Mariusz Kowalski, Driver", rentingEmployeeText);
        }

        [Fact]
        public async Task RentalsTest_ClickButtonAddAndFillingForm_ShouldCauseAddingNewRecord()
        {
            await _page.ClickAsync("#PageRentals");
            await _page.ClickAsync("a[href='/Rentals/Create']");

            await _page.SelectOptionAsync("#selectVehicle", new[] { "Skoda Fabia, WE 31DA3" });
            await _page.SelectOptionAsync("#selectEmployee", new[] { "Andrzej Roman Dobrzański" });
            await _page.FillAsync("input[name='RentalDate']", "01.11.2024");
            await _page.FillAsync("input[name='PlannedReturningDate']", "2024-11-11T00:00");
            await _page.ClickAsync("input[type='submit']");

            var rows = await _page.QuerySelectorAllAsync("table.table tbody tr");
            Assert.NotEmpty(rows);

            bool rentalFound = false;
            foreach (var row in rows)
            {
                var cells = await row.QuerySelectorAllAsync("td");
                var rentedVehicleText = await cells[0].InnerTextAsync();
                var rentingEmployeeText = await cells[1].InnerTextAsync();
                if (rentedVehicleText == "Skoda Fabia, WE 31DA3" && rentingEmployeeText == "Andrzej Roman Dobrzański, Manager")
                {
                    rentalFound = true;
                    break;
                }
            }

            Assert.True(rentalFound);
        }

        [Fact]
        public void RentalsTest_ClickButtonModifyWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RentalsTest_ClickButtonModifyWithSelectedRecordAndFillingForm_ShouldCauseModificationOfSelectedRecord()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RentalsTest_ClickButtoDeleteWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void RentalsTest_ClickButtonDeleteWithSelectedRecordAndFillingForm_ShouldCauseRemovalOfSelectedRecord()
        {
            throw new NotImplementedException();
        }
    }
}