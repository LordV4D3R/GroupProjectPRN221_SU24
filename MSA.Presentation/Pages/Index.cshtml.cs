using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using Services;

namespace MSA.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public IndexModel(ILogger<IndexModel> logger, IProductService productService, IBatchService batchService)
        {
            _logger = logger;
            _productService = productService;
            _batchService = batchService;
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
            Product = _productService.GetAll().Where(x => x.IsDeleted == false).ToList();
            foreach (var product in Product)
            {
                var quantity = _batchService.GetAllByProductId(product.Id).Sum(x => x.Quantity);
                if (quantity == 0 && product.Status == ProductStatus.InStock)
                {
                    product.Status = ProductStatus.OutOfStock;
                    _productService.Update2(product);
                }
            }
            ProductViewModel = Product.Select(product => new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = _batchService.GetAllByProductId(product.Id).Sum(x => x.Quantity),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            }).ToList();
        }
    }
}
