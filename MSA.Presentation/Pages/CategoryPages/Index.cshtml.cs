﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.CategoryPages
{
    public class IndexModel : PageModel
    {
        private readonly MSA.Infrastructure.ApplicationDbContext _context;

        public IndexModel(MSA.Infrastructure.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categorys.ToListAsync();
        }
    }
}
