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
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
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
                ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "CategoryName");
                return Page();
            }

        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public IFormFile ProductImage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var existingProduct = _productService.GetAll().FirstOrDefault(p => p.ProductName == Product.ProductName);

            if (existingProduct != null)
            {
                ModelState.AddModelError("Product.ProductName", "Product name already exists.");
                ViewData["CategoryId"] = new SelectList(_categoryService.GetAll(), "Id", "CategoryName");
                return Page();
            }
            if (ProductImage != null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "Milk");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ProductImage.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProductImage.CopyToAsync(fileStream);
                }

                Product.ImageUrl = "/img/Milk/" + uniqueFileName; // Save the relative path
            }
            _productService.Add(Product);
            _productService.Save();
            return RedirectToPage("./Index");
        }
    }
}
