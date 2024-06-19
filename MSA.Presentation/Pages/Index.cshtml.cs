using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Domain.Dtos.Product;
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
        public ProductViewModel ProductViewModel { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        private void LoadData()
        {
            var product = _productService.GetAll;
            ProductViewModel = new ProductViewModel
            {
                ProductName = "Product 1",
                Price = 100000,
                Description = "Description 1",
                ImageUrl = "https://via.placeholder.com/150",
                TotalQuantity = 10
            };
        }
    }
}
