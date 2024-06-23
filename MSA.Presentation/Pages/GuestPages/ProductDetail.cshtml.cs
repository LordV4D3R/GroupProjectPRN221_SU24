using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Domain.Entities;
using Services;

namespace MSA.Presentation.Pages.GuestPages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductDetailModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IList<Product> Product { get; set; } = default!;
        public List<string> CategoryName { get; set; } = new List<string>();

        public async Task OnGetAsync()
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
