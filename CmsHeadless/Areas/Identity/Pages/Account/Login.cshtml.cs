// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CmsHeadless.Controllers;
using Microsoft.AspNet.Identity;
using CmsHeadless.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace CmsHeadless.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<CmsUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly LogListController _logController;
        private readonly CmsHeadlessDbContext _contextDb;

        public LoginModel(SignInManager<CmsUser> signInManager, ILogger<LoginModel> logger, LogListController logController, CmsHeadlessDbContext contextDb)
        {
            _signInManager = signInManager;
            _logger = logger;
            _logController = logController;
            _contextDb = contextDb;
        }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            //Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //string psw = _contextDb.CmsUser.Where(c => c.Email == Input.Email).Select(c => c.PasswordHash).ToString();
                var tempUsername = _contextDb.CmsUser.Where(c => c.Email == Input.Email).Select(c => c.UserName).ToList();
                string username = "";
                if(tempUsername.Count > 0)
                {
                    username = tempUsername.First();
                }
                var result = await _signInManager.PasswordSignInAsync(username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    _logController.SaveLog(Input.Email, LogListController.LoginSuccessfulCode, "L'utente " + Input.Email + " si sta loggando ", "Login successful", HttpContext);
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    _logController.SaveLog(Input.Email, LogListController.UnclassifiedWarningCode, "L'utente " + Input.Email + " si sta loggando ", "Locked out", HttpContext);
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    if (username == "")
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt - Email errata");
                        _logController.SaveLog(Input.Email, LogListController.LoginWrongUsernameWarningCode, "L'utente " + Input.Email + " si sta loggando ", "Invalid email address", HttpContext);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt - Password errata");
                        _logController.SaveLog(Input.Email, LogListController.LoginWrongPasswordWarningCode, "L'utente " + Input.Email + " si sta loggando ", "Invalid password", HttpContext);
                    }
                    return Page();
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
