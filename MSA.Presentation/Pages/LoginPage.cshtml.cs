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


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            var adminUsername = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Username").Value;
            var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Password").Value;

            if (AccountLoginDto.Username == adminUsername || AccountLoginDto.Password == adminPassword) {
                HttpContext.Session.SetString("role", "Admin");
                return RedirectToPage("/Admin/AccountPages/Index");
            }

            var account = _accountService.GetAccountByUsernameAndPassword(AccountLoginDto);



            if (AccountLoginDto.Username == null || AccountLoginDto.Password == null)
            {
                ErrorMessage = "Không được để trống Username hoặc Password";
                return Page();
            }
            else
            {
                if (account != null)
                {
                    HttpContext.Session.SetString("role", account.Role.ToString());
                    switch (account.Role.ToString())
                    {
                        case "Staff":
                            return RedirectToPage("/Index");
                        case "Customer":
                            return RedirectToPage("/Index");
                        default:
                            ErrorMessage = "Hệ thống xảy ra lỗi, vui lòng thử lại";
                            return Page();
                    }
                }
                else
                {
                    ErrorMessage = "Tài khoản không tồn tại hoặc sai mật khẩu";
                    return Page();

                }
            }
        }
    }
}