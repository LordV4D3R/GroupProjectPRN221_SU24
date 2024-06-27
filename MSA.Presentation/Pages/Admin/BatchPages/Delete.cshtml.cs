using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.Admin.BatchPages
{
    public class DeleteModel : PageModel
    {
        private readonly MSA.Infrastructure.ApplicationDbContext _context;

        public DeleteModel(MSA.Infrastructure.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Batch Batch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batchs.FirstOrDefaultAsync(m => m.Id == id);

            if (batch == null)
            {
                return NotFound();
            }
            else
            {
                Batch = batch;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batchs.FindAsync(id);
            if (batch != null)
            {
                Batch = batch;
                _context.Batchs.Remove(Batch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
