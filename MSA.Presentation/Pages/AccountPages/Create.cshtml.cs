using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Account;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.AccountPages
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public CreateAccountRequest CreateAccountRequest { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _accountService.Add(Account);
            _accountService.Save();
            return RedirectToPage("./Index");
        }
    }
}
