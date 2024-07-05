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

        public IList<Category> Category { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else 
            {  
                if (!string.IsNullOrEmpty(SearchString))
                {
                    Category = _categoryService.SearchByName(SearchString).ToList();
                }
                else
                {
                    Category = _categoryService.GetAll().Where(c => c.IsDeleted == false).ToList();
                }
                return Page();
            }
        }
    }
}
