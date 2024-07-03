using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.StaffPage.ProductPages
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;
        private readonly ICategoryService _categoryService;

        public DeleteModel(IProductService productService, IBatchService batchService, ICategoryService categoryService)
        {
            _productService = productService;
            _batchService = batchService;
            _categoryService = categoryService;
        }
        [BindProperty]
        public Product Product { get; set; } = default!;
        public string CategoryName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
                var category = _categoryService.GetById(product.CategoryId);
                CategoryName = category?.CategoryName ?? "Unknown";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
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

            var product = _productService.GetById(id);
            var hasBatch = _batchService.GetAll().Where(p => p.ProductId == id && p.IsDeleted == false);
            if (product != null)
            {                
                if (hasBatch.IsNullOrEmpty())
                {
                    product.IsDeleted = true;
                    Product = product;
                    _productService.Update(Product);
                    _productService.Save();
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
