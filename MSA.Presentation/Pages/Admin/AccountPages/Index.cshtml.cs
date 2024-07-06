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

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Account = _accountService.SearchByName(SearchString).ToList();
            }
            else
            {
                Account = _accountService.GetAll().Where(x => x.IsDeleted == false).ToList();
            }
        }
    }
}
