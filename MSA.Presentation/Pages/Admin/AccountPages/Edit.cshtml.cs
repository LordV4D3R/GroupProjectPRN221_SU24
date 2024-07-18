using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Domain.Account;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;
using MSA.Presentation.Extensions;

namespace MSA.Presentation.Pages.AccountPages
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EditModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
            if (current == null || current.Role != RoleType.Admin)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return Redirect("/LoginPage");
            }
            else if (id == null)
            {
                return NotFound();
            }

            var account =  _accountService.GetById(id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
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
            _accountService.Update2(Account);
            return RedirectToPage("./Index");
        }
    }
}
