using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.ProductPages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public IndexModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IList<Product> Product { get;set; } = default!;
        public List<string> CategoryName { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Product = _productService.SearchByName(SearchString).ToList();
            }
            else
            {
                Product = _productService.GetAll().ToList();
                foreach (var product in Product)
                {
                    var category = _categoryService.GetById(product.CategoryId);
                    CategoryName.Add(category?.CategoryName ?? "Unknown");
                }
            }
        }
    }
}
