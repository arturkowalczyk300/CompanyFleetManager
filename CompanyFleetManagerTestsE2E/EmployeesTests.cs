using Microsoft.Playwright;

namespace CompanyFleetManagerTestsE2E
{
    public class EmployeesTests : IAsyncLifetime
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
        public async Task EmployeesTest_OnLoad_ShouldDisplayEmployees()
        {
            await _page.ClickAsync("#PageEmployees");

            var rows = await _page.QuerySelectorAllAsync("table.table tbody tr");
            Assert.NotEmpty(rows);

            var firstRowCells = await rows.First().QuerySelectorAllAsync("td");
            var occupationText = await firstRowCells[0].InnerTextAsync();
            var forenameText = await firstRowCells[2].InnerTextAsync();

            Assert.Equal("Driver", occupationText);
            Assert.Equal("Janusz", forenameText);
        }

        [Fact]
        public async Task EmployeesTest_ClickButtonAddAndFillingForm_ShouldCauseAddingNewRecord()
        {
            await _page.ClickAsync("#PageEmployees");
            await _page.ClickAsync("a[href='/Employees/Create']");

            await _page.FillAsync("input[name='Occupation']", "Technician");
            await _page.FillAsync("input[name='Address']", "Oława ul. Wiśniowa 12/3");
            await _page.FillAsync("input[name='Forename']", "Jakub");
            await _page.FillAsync("input[name='Middlename']", "Mariusz");
            await _page.FillAsync("input[name='Surname']", "Kowalski");
            await _page.FillAsync("input[name='NationalIdentityNumber']", "837174628");
            await _page.FillAsync("input[name='WorkPhoneNumber']", "874123084");
            await _page.FillAsync("input[name='PrivatePhoneNumber']", "432503931");
            await _page.FillAsync("input[name='DrivingLicenseCategories']", "B");
            await _page.FillAsync("input[name='DrivingLicenseValidity']", "3.10.2035");
            await _page.FillAsync("input[name='HiredUntil']", "31.10.2025");
            await _page.ClickAsync("input[type='submit']");

            var rows = await _page.QuerySelectorAllAsync("table.table tbody tr");
            Assert.NotEmpty(rows);

            bool employeeFound = false;
            foreach (var row in rows)
            {
                var cells = await row.QuerySelectorAllAsync("td");
                var occupationText = await cells[0].InnerTextAsync();
                var forenameText = await cells[2].InnerTextAsync();
                if (occupationText == "Technician" && forenameText == "Jakub")
                {
                    employeeFound = true;
                    break;
                }
            }

            Assert.True(employeeFound);
        }

        [Fact]
        public void EmployeesTest_ClickButtonModifyWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EmployeesTest_ClickButtonModifyWithSelectedRecordAndFillingForm_ShouldCauseModificationOfSelectedRecord()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EmployeesTest_ClickButtoDeleteWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EmployeesTest_ClickButtonDeleteWithSelectedRecordAndFillingForm_ShouldCauseRemovalOfSelectedRecord()
        {
            throw new NotImplementedException();
        }
    }
}