// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using CompanyFleetManagerWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CompanyFleetManagerWebApp.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly WebServiceAuthenticationApi _webService;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(ILogger<LogoutModel> logger, WebServiceAuthenticationApi webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _webService.LogoutAsync();

            if (result)
            {
                _logger.LogInformation("User logged out.");
                return LocalRedirect(returnUrl);
            }
            else
            {
                _logger.LogInformation("Logging out failed.");
                return Page();
            }
        }
    }
}
