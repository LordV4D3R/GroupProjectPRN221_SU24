using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Account;
using MSA.Domain.Dtos.Account;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;
using MSA.Presentation.Extensions;

namespace MSA.Presentation.Pages.AccountPages
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
            if (current == null || current.Role != RoleType.Admin)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return Redirect("/LoginPage");
            }
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public CreateAccountRequest request { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(CreateAccountRequest request)
        {
            var existingAccountName = _accountService.GetAll().FirstOrDefault(a => a.Username == Account.Username);
            var existingAccountEmail = _accountService.GetAll().FirstOrDefault(b => b.Email == Account.Email);
            if (existingAccountName != null)
            {
                ModelState.AddModelError("Account.Username", "Account name exist");
                return Page();
            }
            if (existingAccountEmail != null)
            {
                ModelState.AddModelError("Account.Email", "Account Email exist");
                return Page();
            }
            Account newAccount = new Account
            {
                Username = request.Username,              
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                ImageUrl = request.ImageUrl,
                Role = request.Role,
                Status = AccountStatus.Active,
            };
            _accountService.Add(newAccount);
            _accountService.Save();

            return RedirectToPage("./Index");
        }
    }
}
