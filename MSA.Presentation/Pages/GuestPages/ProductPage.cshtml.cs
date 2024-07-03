using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Application.IRepositories;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Entities;
using Services;

namespace MSA.Presentation.Pages.GuestPages
{
    public class ProductPageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;

        public ProductPageModel(ILogger<IndexModel> logger,
            IProductService productService)
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
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            }).ToList();
        }
    }
}
