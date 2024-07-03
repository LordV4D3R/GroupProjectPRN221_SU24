using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.CategoryPages
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public DeleteModel(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [BindProperty]
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

            var category = _categoryService.GetById(id);

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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById(id);
            var hasProduct = _productService.GetAll().Where(p => p.CategoryId == id && p.IsDeleted == false);
            if (category != null)
            {               
                if (hasProduct.IsNullOrEmpty())
                {
                    category.IsDeleted = true;
                    Category = category;
                    _categoryService.Update(Category);
                    _categoryService.Save();
                }
                else
                {
                    return RedirectToPage("./Index");
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
