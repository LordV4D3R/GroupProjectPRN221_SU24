using System;
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
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            {
                return Page();
            }          
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var existingCategory = _categoryService.GetAll().FirstOrDefault(p => p.CategoryName == Category.CategoryName);

            if (existingCategory != null)
            {
                ModelState.AddModelError("Category.CategoryName", "Category name already exists.");
                return Page();
            }

            _categoryService.Add(Category);
            _categoryService.Save();
            return RedirectToPage("./Index");
        }
    }
}
