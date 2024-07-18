using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;
using MSA.Presentation.Extensions;

namespace MSA.Presentation.Pages.AccountPages
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
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

            var account = _accountService.GetById(id);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                Account = account;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = _accountService.GetById(id);
            if (account != null)
            {
                account.IsDeleted = true;
                Account = account;
                _accountService.Update(Account);
                _accountService.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
