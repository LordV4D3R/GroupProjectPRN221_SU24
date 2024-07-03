using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.ProductPages
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public EditModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else if (id == null)
            {
                return NotFound();
            }

            var product =  _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {     
                _productService.Update2(Product);
            return RedirectToPage("./Index");
        }

        private bool ProductExists(Guid id)
        {
            return _productService.GetById(id) == null!;
        }
    }
}
