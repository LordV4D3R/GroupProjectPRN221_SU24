using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.ProductPages
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBatchService _batchService;

        public DetailsModel(IProductService productService, ICategoryService categoryService, IBatchService batchService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _batchService = batchService;   
        }

        public Product Product { get; set; } = default!;
        public string CategoryName { get; set; } = string.Empty;
        public IList<Batch> Batch { get; set; } = default!;
        public List<string> ProductName { get; set; } = new List<string>();
        public Guid productId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
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
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
                var quantity = _batchService.GetAllByProductId(product.Id).Sum(x => x.Quantity);
                if (quantity == 0 && product.Status == ProductStatus.InStock)
                {
                    product.Status = ProductStatus.OutOfStock;
                    _productService.Update2(product);
                }
                if (quantity != 0)
                {
                    product.Status = ProductStatus.InStock;
                    _productService.Update2(product);
                }
                var category = _categoryService.GetById(product.CategoryId);
                CategoryName = category?.CategoryName ?? "Unknown";
                Batch = _batchService.GetAll().Where(x => x.ProductId == id && x.IsDeleted == false).ToList();
                productId = id;
                foreach (var batch in Batch)
                {
                    if (batch.Quantity <= 0 || batch.ExpOn <= DateTime.Now)
                    {
                        batch.Status = BatchStatus.Inactive;
                    }
                    else
                    {
                        batch.Status = BatchStatus.Active;
                    }
                    _batchService.Update2(batch);
                    var name = _productService.GetById(batch.ProductId);
                    ProductName.Add(name?.ProductName ?? "Unknown");
                }
            }
            return Page();
        }
    }
}
