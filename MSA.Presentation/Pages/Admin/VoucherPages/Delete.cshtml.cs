using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using MSA.Infrastructure.Services;

namespace MSA.Presentation.Pages.VoucherPages
{
    public class DeleteModel : PageModel
    {
        private readonly IVoucherService _context;

        public DeleteModel(IVoucherService context)
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

            var voucher = _context.GetById(id);

            if (voucher == null)
            {
                return NotFound();
            }
            else
            {
                Voucher = voucher;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = _context.GetById(id);
            if (voucher != null)
            {
                Voucher = voucher;
                _context.Delete(Voucher);
            }

            return RedirectToPage("./Index");
        }
    }
}
