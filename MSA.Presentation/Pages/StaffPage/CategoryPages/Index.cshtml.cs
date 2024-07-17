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
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> Category { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            const int pageSize = 5;

            IEnumerable<Category> categoriesQuery = _categoryService.GetAll().Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(SearchString))
            {
                categoriesQuery = categoriesQuery.Where(c => c.CategoryName.Contains(SearchString));
            }

            int totalCategories = categoriesQuery.Count();
            TotalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);

            Category = categoriesQuery
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Page();
        }
    }
}
