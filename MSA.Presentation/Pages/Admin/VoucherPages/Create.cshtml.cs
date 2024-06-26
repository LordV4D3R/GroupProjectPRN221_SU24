using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using MSA.Infrastructure.Services;

namespace MSA.Presentation.Pages.VoucherPages
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherService _context;

        public CreateModel(IVoucherService context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StaffId"] = new SelectList(_context.GetAll(), "Id", "Address");
            return Page();
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Add(Voucher);

            return RedirectToPage("./Index");
        }
    }
}
