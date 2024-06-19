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

namespace MSA.Presentation.Pages.ProductPages
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public DetailsModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

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
    }
}
