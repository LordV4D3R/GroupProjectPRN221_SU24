using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Entities;
using Services;

namespace MSA.Presentation.Pages.GuestPages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBatchService _batchService;

        public ProductDetailModel(IProductService productService,
            ICategoryService categoryService,
            IBatchService batchService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _batchService = batchService;
        }

        public Product Product { get; set; } = default!;
        public ProductViewModel ProductViewModel { get; set; } = default!;

        public List<string> CategoryName { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = _productService.GetById(id);
            ProductViewModel = new ProductViewModel
            {
                ProductId = Product.Id,
                ProductName = Product.ProductName,
                Price = Product.Price,
                Description = Product.Description,
                ImageUrl = Product.ImageUrl,
                TotalQuantity = _batchService.GetAllByProductId(Product.Id).Sum(x => x.Quantity),
                Status = Product.Status
                };

            return Page();


        }
    }
}
