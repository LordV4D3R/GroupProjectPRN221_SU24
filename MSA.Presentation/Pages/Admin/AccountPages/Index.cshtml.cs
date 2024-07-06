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
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<Account> Account { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Admin" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchString))
                {
                    Account = _accountService.SearchByName(SearchString).ToList();
                }
                else
                {
                    Account = _accountService.GetAll().ToList();
                }
                return Page();
            }
        }
    }
}
