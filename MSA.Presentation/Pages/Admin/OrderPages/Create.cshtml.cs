using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _context;
        private readonly IAccountService _accountService;

        public CreateModel(IOrderService context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            ViewData["CustomerId"] = new SelectList(_accountService.GetAll(), "Id", "Address");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Add(Order);
            _context.Save();

            return RedirectToPage("./Index");
        }
    }
}
