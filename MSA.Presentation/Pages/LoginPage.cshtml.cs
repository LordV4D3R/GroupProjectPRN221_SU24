using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Account;

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

        [BindProperty]
        public AccountLoginDto AccountLoginDto { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            //var adminUsername = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Email").Value;
            //var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Password").Value;
            var account = _accountService.GetAccountByUsernameAndPassword(AccountLoginDto);
            if (AccountLoginDto.Username == null || AccountLoginDto.Password == null)
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
                            ErrorMessage = "Invalid username or password";
                            return Page();
                    }
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                    return Page();

                }
            }
        }
    }
}