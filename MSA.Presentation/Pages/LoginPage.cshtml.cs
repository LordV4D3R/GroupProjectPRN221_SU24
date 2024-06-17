using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Application.IServices;

namespace MSA.Presentation.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ILogger<LoginPageModel> _logger;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginPageModel(ILogger<LoginPageModel> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostLogin(string username, string password)
        {
            //var adminUsername = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Email").Value;
            //var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Password").Value;
            var account = _accountService.GetAccountByUsernameAndPassword(username, password);
            if (username == null || password == null)
            {
                return RedirectToPage("/Error");
            }
            else
            {
                if (account != null)
                {
                    HttpContext.Session.SetString("role", account.Role.ToString());
                    switch (account.Role.ToString())
                    {
                        case "Admin":
                            return RedirectToPage("/Index");
                        case "Staff":
                            return RedirectToPage("/Index");
                        case "Customer":
                            return RedirectToPage("/Index");
                        default:
                            return RedirectToPage("/Error");
                    }
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
        }
    }
}