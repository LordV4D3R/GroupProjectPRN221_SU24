using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Common;
using MSA.Domain.Dtos.Account;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using Newtonsoft.Json;

namespace MSA.Presentation.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ILogger<LoginPageModel> _logger;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginPageModel(ILogger<LoginPageModel> logger,
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public AccountLoginDto AccountLoginDto { get; set; } = default!;
        public string ErrorMessage { get; set; } = string.Empty;

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


            if (AccountLoginDto.Username == null || AccountLoginDto.Password == null)
            {
                ErrorMessage = "Không được để trống Username hoặc Password";
                return Page();
            }
            else
            {
                if (AccountLoginDto.Username == AppConfig.Admin.Username && AccountLoginDto.Password == AppConfig.Admin.Password)
                {
                    string customerJson = JsonConvert.SerializeObject(new AccountSession
                    {
                        Username = AccountLoginDto.Username,
                        DisplayName = AccountLoginDto.Username,
                        Email = AccountLoginDto.Username,
                        Role = RoleType.Admin,
                    });
                    _httpContextAccessor.HttpContext!.Session.Clear();
                    _httpContextAccessor.HttpContext!.Session.SetString("CurrentUser", customerJson);
                    return Redirect("/Admin/AccountPages/Index");
                }

                var account = _accountService.GetAccountByUsernameAndPassword(AccountLoginDto);


                if (account == null)
                {
                    ErrorMessage = "Tài khoản không tồn tại hoặc sai mật khẩu";
                    return Page();
                }
                else
                {
                    string customerJson = JsonConvert.SerializeObject(new AccountSession
                    {
                        Id = account.Id,
                        Username = account.Username,
                        DisplayName = account.FullName,
                        Email = account.Email,
                        Role = account.Role,
                    });
                    _httpContextAccessor.HttpContext!.Session.Clear();
                    _httpContextAccessor.HttpContext!.Session.SetString("CurrentUser", customerJson);
                    string currentUser = _httpContextAccessor.HttpContext!.Session.GetString("CurrentUser");
                    HttpContext.Session.SetString("role", account.Role.ToString());
                    switch (account.Role.ToString())
                    {
                        case "Staff":
                            return RedirectToPage("/StaffPage/Index");
                        case "Customer":
                            return RedirectToPage("/index");
                        default:
                            ErrorMessage = "Hệ thống xảy ra lỗi, vui lòng thử lại";
                            return Page();
                    }
                }
            }

            //var adminUsername = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Username").Value;
            //var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Password").Value;

            //if (AccountLoginDto.Username == adminUsername || AccountLoginDto.Password == adminPassword) {
            //    HttpContext.Session.SetString("role", "Admin");
            //    return RedirectToPage("/Admin/AccountPages/Index");
            //}

            //var account = _accountService.GetAccountByUsernameAndPassword(AccountLoginDto);

            //if (AccountLoginDto.Username == null || AccountLoginDto.Password == null)
            //{
            //    ErrorMessage = "Không được để trống Username hoặc Password";
            //    return Page();
            //}
            //else
            //{
            //    if (account != null)
            //    {
            //        HttpContext.Session.SetString("role", account.Role.ToString());
            //        switch (account.Role.ToString())
            //        {
            //            case "Staff":
            //                return RedirectToPage("/Index");
            //            case "Customer":
            //                return RedirectToPage("/Index");
            //            default:
            //                ErrorMessage = "Hệ thống xảy ra lỗi, vui lòng thử lại";
            //                return Page();
            //        }
            //    }
            //    else
            //    {
            //        ErrorMessage = "Tài khoản không tồn tại hoặc sai mật khẩu";
            //        return Page();
            //    }
            //}
        }

        //var adminUsername = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Username").Value;
        //var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Password").Value;

        //if (AccountLoginDto.Username == adminUsername || AccountLoginDto.Password == adminPassword) {
        //    HttpContext.Session.SetString("role", "Admin");
        //    return RedirectToPage("/Admin/AccountPages/Index");
        //}

        //var account = _accountService.GetAccountByUsernameAndPassword(AccountLoginDto);

        //if (AccountLoginDto.Username == null || AccountLoginDto.Password == null)
        //{
        //    ErrorMessage = "Không được để trống Username hoặc Password";
        //    return Page();
        //}
        //else
        //{
        //    if (account != null)
        //    {
        //        HttpContext.Session.SetString("role", account.Role.ToString());
        //        switch (account.Role.ToString())
        //        {
        //            case "Staff":
        //                return RedirectToPage("/Index");
        //            case "Customer":
        //                return RedirectToPage("/Index");
        //            default:
        //                ErrorMessage = "Hệ thống xảy ra lỗi, vui lòng thử lại";
        //                return Page();
        //        }
        //    }
        //    else
        //    {
        //        ErrorMessage = "Tài khoản không tồn tại hoặc sai mật khẩu";
        //        return Page();
        //    }
        //}
    }
    }

