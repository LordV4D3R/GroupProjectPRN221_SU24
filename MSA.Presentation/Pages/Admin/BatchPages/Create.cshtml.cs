using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.Admin.BatchPages
{
    public class CreateModel : PageModel
    {
        private readonly MSA.Infrastructure.ApplicationDbContext _context;

        public CreateModel(MSA.Infrastructure.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public Batch Batch { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Batchs.Add(Batch);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
