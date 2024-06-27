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
    public class DetailsModel : PageModel
    {
        private readonly MSA.Infrastructure.ApplicationDbContext _context;

        public DetailsModel(MSA.Infrastructure.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
