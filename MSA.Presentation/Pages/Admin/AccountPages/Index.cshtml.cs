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
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<Account> Account { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
            if (current == null || current.Role != RoleType.Admin)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return Redirect("/LoginPage");
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchString))
                {
                    Account = _accountService.SearchByName(SearchString).ToList();
                }
                else
                {
                    Account = _accountService.GetAll().Where(x => x.IsDeleted == false).ToList();
                }
                return Page();
            }          
        }
    }
}
