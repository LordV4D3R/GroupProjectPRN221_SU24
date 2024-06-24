using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using MSA.Infrastructure.Services;

namespace MSA.Presentation.Pages.VoucherPages
{
    public class EditModel : PageModel
    {
        private readonly IVoucherService _context;

        public EditModel(IVoucherService context)
        {
            _context = context;
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher =  _context.GetById(id);
            if (voucher == null)
            {
                return NotFound();
            }
            Voucher = voucher;
           ViewData["StaffId"] = new SelectList(_context.GetAll(), "Id", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }            
            _context.Update(Voucher);
            return RedirectToPage("./Index");
        }
    }
}
