using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.AccountPages
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;
        public DeleteModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Admin" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            {
                if (id == null)
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
