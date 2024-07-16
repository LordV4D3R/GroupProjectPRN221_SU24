using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Account;
using MSA.Domain.Dtos.Account;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages
{
    public class SignupModel : PageModel
    {
        private readonly IAccountService _accountService;
        public SignupModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public SignUpViewModel request { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(SignUpViewModel request)
        {
            var existingAccountName = _accountService.GetAll().FirstOrDefault(a => a.Username == Account.Username);
            var existingAccountEmail = _accountService.GetAll().FirstOrDefault(b => b.Email == Account.Email);
            if (existingAccountName != null)
            {
                ModelState.AddModelError("Account.Username", "Account Username exist");
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
                ImageUrl = " ",
                Role = RoleType.Customer,
                Status = AccountStatus.Active,
            };
            _accountService.Add(newAccount);
            _accountService.Save();

            return RedirectToPage("/Index");
        }
    }
}
