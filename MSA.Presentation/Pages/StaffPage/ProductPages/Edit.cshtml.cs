using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public IFormFile? ProductImage { get; set; }

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
            var existingProduct = _productService.GetAll().FirstOrDefault(p => p.ProductName == Product.ProductName && p.Id != Product.Id);

            if (existingProduct != null)
            {
                ModelState.AddModelError("Product.ProductName", "Product name already exists.");
                var product = _productService.GetById(Product.Id);
                Product = product;
                ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "CategoryName");
                return Page();
            }
            if (ProductImage != null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "Milk");
                var uniqueFileName = ProductImage.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProductImage.CopyToAsync(fileStream);
                }

                Product.ImageUrl = "img/Milk/" + uniqueFileName; // Save the relative path
            }
            _productService.Update2(Product);
            return RedirectToPage("./Index");
        }
    }
}
