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
    public class IndexModel : PageModel
    {
        private readonly MSA.Infrastructure.ApplicationDbContext _context;

        public IndexModel(MSA.Infrastructure.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Batch> Batch { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Batch = await _context.Batchs
                .Include(b => b.Product).ToListAsync();
        }
    }
}
