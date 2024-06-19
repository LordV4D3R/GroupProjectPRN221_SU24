using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Entities;
using Services;

namespace MSA.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;

        public IndexModel(ILogger<IndexModel> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [BindProperty]
        public IList<ProductViewModel> ProductViewModel { get; set; } = default!;
        public IList<Product> Product { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            LoadData();
            return Page();
        }

        private void LoadData()
        {
            Product = _productService.GetAll().ToList();
            ProductViewModel = Product.Select(product => new ProductViewModel
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                TotalQuantity = product.TotalQuantity,
                Status = product.Status

            }).ToList();
        }
    }
}
