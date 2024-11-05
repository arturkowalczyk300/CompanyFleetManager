using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace CompanyFleetManagerTestsE2E
{
    public class VehiclesTests : IAsyncLifetime
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
        public async void VehiclesTest_OnLoad_ShouldDisplayVehicles()
        {
            await _page.ClickAsync("#PageVehicles");

            var rows = await _page.QuerySelectorAllAsync("table.table tbody tr");

            Assert.NotEmpty(rows);

            var firstRowCells = await rows.First().QuerySelectorAllAsync("td");
            var modelText = await firstRowCells[1].InnerTextAsync();
            var licencePlateText = await firstRowCells[2].InnerTextAsync();

            Assert.Equal("A6", modelText);
            Assert.Equal("DW 321AB", licencePlateText);
        }

        [Fact]
        public async void VehiclesTest_ClickButtonAddAndFillingForm_ShouldCauseAddingNewRecord()
        {
            await _page.ClickAsync("#PageVehicles");

            await _page.ClickAsync("a[href='/Vehicles/Create']");

            await _page.FillAsync("input[name='Brand']", "Fiat");
            await _page.FillAsync("input[name='Model']", "Ducato");
            await _page.FillAsync("input[name='LicencePlateNumber']", "WE XA023");
            await _page.FillAsync("input[name='ProductionYear']", "2021");
            await _page.FillAsync("input[name='Mileage']", "123016");
            await _page.FillAsync("input[name='VehicleInspectionValidity']", "01.11.2025");
            await _page.CheckAsync("input[name='IsDamaged']");

            await _page.ClickAsync("input[type='submit']");

            var rows = await _page.QuerySelectorAllAsync("table.table tbody tr");

            Assert.NotEmpty(rows);

            bool vehicleFound = false;
            foreach (var row in rows)
            {
                var cells = await row.QuerySelectorAllAsync("td");
                var modelText = await cells[1].InnerTextAsync();
                var licencePlateText = await cells[2].InnerTextAsync();
                if (modelText == "Ducato" && licencePlateText == "WE XA023")
                {
                    vehicleFound = true;
                    break;
                }
            }

            Assert.True(vehicleFound);
        }

        [Fact]
        public void VehiclesTest_ClickButtonModifyWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void VehiclesTest_ClickButtonModifyWithSelectedRecordAndFillingForm_ShouldCauseModificationOfSelectedRecord()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void VehiclesTest_ClickButtoDeleteWhenNoRecordSelected_ShouldDisplayErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void VehiclesTest_ClickButtonDeleteWithSelectedRecordAndFillingForm_ShouldCauseRemovalOfSelectedRecord()
        {
            throw new NotImplementedException();
        }
    }
}
