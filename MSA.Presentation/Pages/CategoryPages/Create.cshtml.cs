﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.CategoryPages
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _context;

        public CreateModel(ICategoryService context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Add(Category);
            _context.Save();
            return RedirectToPage("./Index");
        }
    }
}
