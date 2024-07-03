using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.CategoryPages
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _context;

        public DetailsModel(ICategoryService context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
